using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TDS.WalletService.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDS.WalletService.Infrastructure.Persistence.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddRepository(this IServiceCollection services, string connectionStrng)
        {
           
            

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionStrng, b => b.MigrationsAssembly("WalletService.API"));
            });
        }
    }
}
