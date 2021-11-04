using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TDS.IdentityService.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDS.IdentityService.Infrastructure.Persistence.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddRepository(this IServiceCollection services, string connectionStrng)
        {
           
            

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionStrng, b => b.MigrationsAssembly("IdentityService.API"));
            });
        }
    }
}
