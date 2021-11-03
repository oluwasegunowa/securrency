
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Entities;
using System.Threading.Tasks;

namespace SecurrencyTDS.IdentityService.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Repository<User> userRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IRepository<User> UserRepository => userRepository ?? new Repository<User>(_context);

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
