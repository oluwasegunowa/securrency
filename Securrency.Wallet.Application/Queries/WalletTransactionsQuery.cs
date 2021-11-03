using MediatR;
using Securrency.Domain.Response;
using SecurrencyTDS.WalletManager.Application.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace SecurrencyTDS.WalletManager.Application.Command
{


    public class WalletTransactionsQuery : IRequest<GenericResponse<MemoryStream>>
    {
        [Required]
        public string WalletAddress { get; set; }
    }

    

}
