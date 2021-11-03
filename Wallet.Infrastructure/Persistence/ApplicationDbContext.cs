using Microsoft.EntityFrameworkCore;
using SecurrencyTDS.WalletManager.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurrencyTDS.WalletManager.Infrastructure.Persistence
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
