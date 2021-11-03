
using SecurrencyTDS.Domain.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurrencyTDS.WalletManager.Infrastructure.Integrations
{


    public interface IWalletTransactionDiscoveryService
    {

        Task<List<StellarWalletPayment>> GetLatestTransactions(string WalletAddress, string WalletLastCursor);

    }

}
