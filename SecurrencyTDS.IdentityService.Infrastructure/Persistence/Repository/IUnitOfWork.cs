
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Entities;
using System;
using System.Threading.Tasks;

namespace SecurrencyTDS.IdentityService.Infrastructure.Persistence.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get;}

        Task<bool> Complete();
    }
}
