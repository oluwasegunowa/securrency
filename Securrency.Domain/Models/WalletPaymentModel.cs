using System;
using System.Collections.Generic;
using System.Text;

namespace SecurrencyTDS.Domain.Models
{
    public class StellarWalletPayment
    {
        public string TxnId { get;   set; }
        public string Sender { get;   set; }
        public string Receiver { get;   set; }
        public double TxnAmount { get;   set; }
        public DateTime TxnDate { get;   set; }
        public string PagingToken { get;   set; }
        public string SourceAccount { get;   set; }
        public string TransactionHash { get;   set; }
        public string AssetCode { get;   set; }
        public bool TransactionSuccessful { get;   set; }
    }

}
