using TDS.WalletService.Infrastructure.Persistence.Entities;
using System;
using System.Threading.Tasks;

namespace TDS.WalletService.Infrastructure.Persistence.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Wallet> WalletRepository { get;}
        IRepository<WalletTransaction> WalletTransactionRepository { get; }

        Task<bool> Complete();
    }
}
