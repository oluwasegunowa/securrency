using MediatR;
using SecurrencyTDS.WalletManager.Infrastructure.Integrations;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Repository;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using SecurrencyTDS.WalletManager.Application.Command;
using SecurrencyTDS.WalletManager.Application.Response;

namespace SecurrencyTDS.WalletManager.Application.Handlers
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

                foreach(var wallet in allActiveWallets)
                {
                    var returnTransactions = await _walletTransactionDiscoveryService.GetLatestTransactions(wallet.Address, wallet.LastTransactionDiscoveryToken);
                    if (!returnTransactions.Any())
                    {
                        continue;
                    }

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
            return new DiscoveryResponse() { Message = $"New transactions discovered." };


        }
    }
}
