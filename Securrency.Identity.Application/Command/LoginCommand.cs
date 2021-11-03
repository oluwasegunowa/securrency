using Identity.Application.Response;
using MediatR;
using Securrency.Domain.Response;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Securrency.Identity.Application.Command
{
    
    public class LoginCommand : IRequest<GenericResponse< LoginResponse>>
    {


        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


    }


  

}
