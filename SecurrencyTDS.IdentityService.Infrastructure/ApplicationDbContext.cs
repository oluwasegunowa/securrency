

using Microsoft.EntityFrameworkCore;
using SecurrencyTDS.IdentityService.Infrastructure.Persistence.Entities;
using SecurrencyTDS.IdentityService.Infrastructure.Seed;

namespace SecurrencyTDS.IdentityService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        
  public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

      
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>().HasData( UserSeed.GetSeed());
            base.OnModelCreating(modelBuilder);

        }


    }
}
