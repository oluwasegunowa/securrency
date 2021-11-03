using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Identity.Application.Response;
using Securrency.Domain.Response;
using Securrency.Identity.Application.Command;

namespace IdentityService.Controllers
{
    /// <summary>
    /// This controller manages the generation of authentication token after a successful login.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {

        private readonly ILogger<IdentityController> _logger;
        private readonly IMediator _mediator;

        public IdentityController(ILogger<IdentityController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }






        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {


            var result = await _mediator.Send(command);
            if (!result.IsSuccessful)
            {
                return new BadRequestObjectResult(new { Message = result.Message });

            }
            return Ok(result?.ResponseModel);


        }


    }
}
