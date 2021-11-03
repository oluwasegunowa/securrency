using MediatR;
using Securrency.Domain.Response;
using SecurrencyTDS.WalletManager.Application.Command;
using SecurrencyTDS.WalletManager.Application.Response;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurrencyTDS.WalletManager.Application.Handlers
{
    public class WalletTransactionsQueryHandler : IRequestHandler<WalletTransactionsQuery, GenericResponse<MemoryStream>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public WalletTransactionsQueryHandler( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        public async  Task<GenericResponse<MemoryStream>> Handle(WalletTransactionsQuery request, CancellationToken cancellationToken)
        {


            if (!(await _unitOfWork.WalletRepository.Find(c => c.Address == request.WalletAddress)).Where(s => s.Address == request.WalletAddress).Any())
            {
                return new GenericResponse<MemoryStream>() { IsSuccessful=false, Message="The wallet has not been uploaded. You can only view report for wakkets that you have uploaded." };
            }


            var walletTransactions = (await _unitOfWork.WalletTransactionRepository.Find(c =>(c.Sender == request.WalletAddress || c.Receiver == request.WalletAddress)))
             .Select(s => new
             {
                 Amount= (s.Sender == request.WalletAddress ? -s.TxnAmount : s.TxnAmount),
                 Wallet = s.Sender == request.WalletAddress ? s.Receiver : s.Sender
             }).ToList();


                                
            var transactions = walletTransactions.GroupBy(g=>g.Wallet).Select(s=> new WalletTransactionResponse { Amount =s.Sum(s2=>s2.Amount), Wallet = s.Key }).ToList();


            var stream = new MemoryStream();
            using (var streamWriter = new StreamWriter(stream, leaveOpen: true))
            {
                await streamWriter.WriteLineAsync(
                $"Wallet, Amount"
              );
                foreach (var p in transactions)
                {
                    await streamWriter.WriteLineAsync(
                      $"{p.Wallet}, {p.Amount}"
                    );
                    await streamWriter.FlushAsync();
                }
            }


            return new GenericResponse<MemoryStream>() {  IsSuccessful=true, Message="Transactions retrieved", ResponseModel=stream};

        }
    }
}
