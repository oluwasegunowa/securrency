using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurrencyTDS. WalletManager.Infrastructure.Persistence.Entities
{
    public class WalletTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string TxnId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public double TxnAmount { get; set; }
        public DateTime TxnDate { get; set; }
        public string PagingToken { get; set; }
        public string SourceAccount { get; set; }
        public string TransactionHash { get; set; }
        public string AssetCode { get; set; }
        public bool TransactionSuccessful { get; set; }

    }
}
