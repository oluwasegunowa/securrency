

using Microsoft.EntityFrameworkCore;
using TDS.IdentityService.Infrastructure.Persistence.Entities;
using TDS.IdentityService.Infrastructure.Seed;

namespace TDS.IdentityService.Infrastructure.Persistence
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
