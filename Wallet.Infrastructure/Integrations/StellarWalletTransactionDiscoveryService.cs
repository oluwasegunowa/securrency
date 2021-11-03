
using Microsoft.Extensions.Options;
using SecurrencyTDS.Domain.Authorization;
using SecurrencyTDS.Domain.Models;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecurrencyTDS. WalletManager.Infrastructure.Integrations
{
    public class StellarWalletTransactionDiscoveryService : IWalletTransactionDiscoveryService
    {
        private readonly IOptions<StellarSettings> _stellarSettings;

        public StellarWalletTransactionDiscoveryService(IOptions<StellarSettings> stellarSettings)
        {
            _stellarSettings = stellarSettings;

        }


       
        public async Task<List<StellarWalletPayment>> GetLatestTransactions(string WalletAddress, string WalletLastCursor)
        {
            try
            {



                using (var server = new Server(_stellarSettings.Value.ServerUrl))
                {
                    try
                    {



                        //"GC2BKLYOOYPDEFJKLKY6FNNRQMGFLVHJKQRGNSSRRGSMPGF32LHCQVGF"
                        var requestBuilder = server.Payments.ForAccount(WalletAddress).Order(OrderDirection.ASC).Limit(_stellarSettings.Value.FetchLimit);


                        //Fetch data based on the last cursor. It can also be set as now
                        if (!string.IsNullOrEmpty(WalletLastCursor))
                        {
                            requestBuilder.Cursor(WalletLastCursor);
                        }

                      


                        var result = await requestBuilder.Execute();
                        var filteredREsult = result.Records.Where(x => x.Type == _stellarSettings.Value.TransactionType).Select(x => x as PaymentOperationResponse).ToArray();
                        var getNextCursor = filteredREsult.LastOrDefault();


                        //Filter the response based on the configured conrrency XLM
                        var paymentList = filteredREsult.Where(c => c.AssetCode == _stellarSettings.Value.CurrencyFilter || _stellarSettings.Value.CurrencyFilter == null).Select(payment => new StellarWalletPayment()
                        {

                            TxnId = payment.Id.ToString(),
                            AssetCode = payment.AssetCode,
                            Sender = payment.From,
                            Receiver = payment.To,
                            TxnAmount = double.Parse(payment.Amount),
                            TxnDate = DateTime.ParseExact(payment.CreatedAt, new string[] { "MM/dd/yyyy HH:mm:ss", "MM/dd/yy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                            PagingToken = payment.PagingToken,
                            SourceAccount = payment.SourceAccount,
                            TransactionHash = payment.TransactionHash,
                            TransactionSuccessful = payment.TransactionSuccessful

                        }).ToList();

                        return paymentList;


                    }
                    catch (HttpResponseException ex)
                    {
                        if (ex.StatusCode == 404) {

                            //The requested account does not exs=ist.
                           
                        }
                        return new List<StellarWalletPayment>();


                    }

                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                        throw ex;
                           
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }

}
