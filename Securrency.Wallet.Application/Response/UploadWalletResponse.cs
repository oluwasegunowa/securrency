using System;
using System.Collections.Generic;
using System.Text;

namespace SecurrencyTDS.WalletManager.Application.Response
{
    public class UploadWalletResponse
    {
        public int UploadEntriesCount { get; set; }
        public int SuccessfulUpload { get { return UploadEntriesCount - DuplicateEntriesCount; }  }
        public int DuplicateEntriesCount { get; set; }
    }

}
