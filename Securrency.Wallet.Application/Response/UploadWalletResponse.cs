using System;
using System.Collections.Generic;
using System.Text;

namespace SecurrencyTDS.WalletManager.Application.Response
{
    public class UploadWalletResponse
    {
        public int UploadEntriesCount { get; set; }
        public int SuccessfulUpload { get; set; }
        public int DuplicateEntries { get; set; }
    }

}
