using MediatR;
using TDS.Domain.Response;
using TDS.WalletService.Application.Requests;
using TDS.WalletService.Application.Response;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TDS.WalletService.Application.Command
{

   
    public class UploadWalletAddressesCommand:IRequest<GenericResponse<UploadWalletResponse>>
    {
        [Required]
        public List<WalletAddressModel> WalletAddressModels { get; set; }
    }

}
