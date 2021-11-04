using MediatR;
using TDS.Domain.Response;
using TDS.WalletService.Application.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace TDS.WalletService.Application.Command
{


    public class WalletTransactionsQuery : IRequest<GenericResponse<MemoryStream>>
    {
        [Required]
        public string WalletAddress { get; set; }
    }

    

}
