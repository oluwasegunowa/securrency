using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecurrencyTDS.WalletManager.Application.Requests
{

    public class WalletAddressModel
    {
        [Required]
        public string Address { get; set; }
    }

}
