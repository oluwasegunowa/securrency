using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TDS.WalletService.Infrastructure.Persistence.Entities
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Address { get; set; }

        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastDiscoveryTimeStamp { get; set; }
        public string LastTransactionDiscoveryToken { get; set; }
    }
}
