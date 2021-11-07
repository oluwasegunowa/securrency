namespace TDS.WalletService.Application.Response
{
    public class DiscoveryResponse
    {
        public string Message { get; internal set; }
        public int NoOfWalletScanned { get; internal set; }
        public int NoOfWalletsWithNewTransactions { get; internal set; }
    }

}
