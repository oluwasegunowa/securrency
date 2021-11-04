using TDS.WalletService.Infrastructure.Persistence.Entities;
using System.Threading.Tasks;

namespace TDS.WalletService.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Repository<Wallet> walletRepository;
        private Repository<WalletTransaction> walletTransactionRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IRepository<Wallet> WalletRepository => walletRepository ?? new Repository<Wallet>(_context);
        public IRepository<WalletTransaction> WalletTransactionRepository => walletTransactionRepository ?? new Repository<WalletTransaction>(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
