
using MediatR;
using TDS.Domain.Response;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TDS.Identity.Application.Response;

namespace TDS.IdentityService.Application.Command
{
    
    public class LoginCommand : IRequest<GenericResponse< LoginResponse>>
    {


        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


    }


  

}
