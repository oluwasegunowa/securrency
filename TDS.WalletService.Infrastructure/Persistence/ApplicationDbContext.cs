using Microsoft.EntityFrameworkCore;
using TDS.WalletService.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDS.WalletService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

      
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
    }
}
