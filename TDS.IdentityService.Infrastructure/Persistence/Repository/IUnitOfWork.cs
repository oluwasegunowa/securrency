
using TDS.IdentityService.Infrastructure.Persistence.Entities;
using System;
using System.Threading.Tasks;

namespace TDS.IdentityService.Infrastructure.Persistence.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get;}

        Task<bool> Complete();
    }
}
