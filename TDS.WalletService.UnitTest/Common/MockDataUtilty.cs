using System.Collections.Generic;
using TDS.Domain.Models;
using TDS.WalletService.Infrastructure.Persistence.Entities;

namespace TDS.WalletService.UnitTest
{
    public class MockDataUtilty
    {

        public static IEnumerable<Wallet> GetWalletList()
        {
            var retList = new List<Wallet>
        {
            new Wallet {Id = 1,   Address = "1111-1111-1111-1111",   IsActive=true},
            new Wallet {Id = 2,  Address = "2222-2222-2222-222",    IsActive=true},
            new Wallet {Id = 3, Address = "3333-33333-3333-333-",  IsActive=true}
        };

            return retList;
        }


        public static IEnumerable<WalletTransaction> GetWalletTransactionList()
        {
            var retList = new List<WalletTransaction>
        {
            new WalletTransaction {Id = 1,  Sender = "1111-1111-1111-1111", Receiver="3333-33333-3333-333-",  AssetCode ="XLM",   TxnAmount=100},
            new WalletTransaction {Id = 2,  Sender = "2222-2222-2222-222",  Receiver= "1111-1111-1111-1111",AssetCode ="XLM",   TxnAmount=100},
            new WalletTransaction {Id = 3, Sender = "3333-33333-3333-333-", Receiver="2222-2222-2222-222", AssetCode ="XLM",   TxnAmount=100},
             new WalletTransaction {Id = 4,  Sender = "1111-1111-1111-1111", Receiver="2222-2222-2222-222", AssetCode ="XLM",     TxnAmount=100},
            new WalletTransaction {Id = 5,  Sender = "2222-2222-2222-222",  Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100},
            new WalletTransaction {Id = 6, Sender = "3333-33333-3333-333-",  Receiver="2222-2222-2222-222", AssetCode ="XLM", TxnAmount=100},
             new WalletTransaction {Id = 7,  Sender = "1111-1111-1111-1111", Receiver="2222-2222-2222-222",AssetCode ="XLM",     TxnAmount=100},
            new WalletTransaction {Id = 8,  Sender = "2222-2222-2222-222",  Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100},
            new WalletTransaction {Id = 9, Sender = "3333-33333-3333-333-",  Receiver="1111-1111-1111-1111",AssetCode ="XLM",  TxnAmount=100},
             new WalletTransaction {Id = 10,  Sender = "1111-1111-1111-1111", Receiver="3333-33333-3333-333-",AssetCode ="XLM",     TxnAmount=100},
            new WalletTransaction {Id = 11,  Sender = "2222-2222-2222-222",  Receiver="3333-33333-3333-333-", AssetCode ="XLM",  TxnAmount=100},
            new WalletTransaction {Id = 12, Sender = "3333-33333-3333-333-", Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100}

        };

            return retList;
        }


        public static IEnumerable<StellarWalletPayment> GetStellarWalletTransactionMockedData()
        {
            var retList = new List<StellarWalletPayment>
            {
                  new StellarWalletPayment {  Sender = "1111-1111-1111-1111", Receiver="3333-33333-3333-333-",  AssetCode ="XLM",   TxnAmount=100},
            new StellarWalletPayment {  Sender = "2222-2222-2222-222",  Receiver= "1111-1111-1111-1111",AssetCode ="XLM",   TxnAmount=100},
            new StellarWalletPayment { Sender = "3333-33333-3333-333-", Receiver="2222-2222-2222-222", AssetCode ="XLM",   TxnAmount=100},
             new StellarWalletPayment {  Sender = "1111-1111-1111-1111", Receiver="2222-2222-2222-222", AssetCode ="XLM",     TxnAmount=100},
            new StellarWalletPayment { Sender = "2222-2222-2222-222",  Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100},
            new StellarWalletPayment { Sender = "3333-33333-3333-333-",  Receiver="2222-2222-2222-222", AssetCode ="XLM", TxnAmount=100},
             new StellarWalletPayment { Sender = "1111-1111-1111-1111", Receiver="2222-2222-2222-222",AssetCode ="XLM",     TxnAmount=100},
            new StellarWalletPayment { Sender = "2222-2222-2222-222",  Receiver="1111-1111-1111-1111", AssetCode ="XLM",  TxnAmount=100},
            new StellarWalletPayment { Sender = "3333-33333-3333-333-",  Receiver="1111-1111-1111-1111",AssetCode ="XLM",  TxnAmount=100},

            };

            return retList;
        }
    }
}
