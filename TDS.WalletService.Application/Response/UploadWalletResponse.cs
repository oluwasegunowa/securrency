using System;
using System.Collections.Generic;
using System.Text;

namespace TDS.WalletService.Application.Response
{
    public class UploadWalletResponse
    {
        public int UploadEntriesCount { get; set; }
        public int SuccessfulUpload { get { return UploadEntriesCount - DuplicateEntriesCount; }  }
        public int DuplicateEntriesCount { get; set; }
    }

}
