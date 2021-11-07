using MediatR;
using TDS.WalletService.Infrastructure.Integrations;
using TDS.WalletService.Infrastructure.Persistence.Repository;
using TDS.WalletService.Infrastructure.Persistence.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using TDS.WalletService.Application.Command;
using TDS.WalletService.Application.Response;

namespace TDS.WalletService.Application.Handlers
{
    public class DiscoverWalletTransactionsHandler : IRequestHandler<DiscoverWalletTransactionsCommand, DiscoveryResponse>
    {
        private readonly IWalletTransactionDiscoveryService _walletTransactionDiscoveryService;
        private readonly IUnitOfWork _unitOfWork;
        public DiscoverWalletTransactionsHandler(IWalletTransactionDiscoveryService walletTransactionDiscoveryService,
            IUnitOfWork unitOfWork)
        {
            _walletTransactionDiscoveryService = walletTransactionDiscoveryService;
            _unitOfWork = unitOfWork;
        }
        public async Task<DiscoveryResponse> Handle(DiscoverWalletTransactionsCommand request, CancellationToken cancellationToken)
        {


            //Retrieve all the wallets and foreach of the wallets get ttansactions


                var allActiveWallets =await  _unitOfWork.WalletRepository.Find(f => f.IsActive == true );
                DiscoveryResponse response = new DiscoveryResponse()
                {
                    NoOfWalletScanned = allActiveWallets.Count(),
                    NoOfWalletsWithNewTransactions=0
                };


            foreach (var wallet in allActiveWallets)
            {
                var returnTransactions = await _walletTransactionDiscoveryService.GetLatestTransactions(wallet.Address, wallet.LastTransactionDiscoveryToken);
                if (!returnTransactions.Any())
                {
                    continue;
                }
                response.NoOfWalletsWithNewTransactions++;
                var lastTransaction = returnTransactions.LastOrDefault();
                wallet.LastTransactionDiscoveryToken = lastTransaction.PagingToken;

                //Auto Mapper can be used for this section
                var walletTransactions = returnTransactions.Select(s => new WalletTransaction
                {

                    AssetCode = s.AssetCode,
                    PagingToken = s.PagingToken,
                    Receiver = s.Receiver,
                    Sender = s.Sender,
                    SourceAccount = s.SourceAccount,
                    TransactionHash = s.TransactionHash,
                    TransactionSuccessful = s.TransactionSuccessful,
                    TxnAmount = s.TxnAmount,
                    TxnDate = s.TxnDate,
                    TxnId = s.TxnId

                }).ToList();


                _unitOfWork.WalletTransactionRepository.AddList(walletTransactions);


                wallet.LastDiscoveryTimeStamp = DateTime.Now;
                await _unitOfWork.WalletRepository.UpdateAsync(wallet);
            }

            await _unitOfWork.Complete();
            response.Message = "Transaction discovery completed successfully";
            return response;


        }
    }
}
