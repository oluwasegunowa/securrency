using MediatR;
using Securrency.Domain.Response;
using SecurrencyTDS.WalletManager.Application.Requests;
using SecurrencyTDS.WalletManager.Application.Response;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecurrencyTDS.WalletManager.Application.Command
{

   
    public class UploadWalletAddressesCommand:IRequest<GenericResponse<UploadWalletResponse>>
    {
        [Required]
        public List<WalletAddressModel> WalletAddressModels { get; set; }
    }

}
