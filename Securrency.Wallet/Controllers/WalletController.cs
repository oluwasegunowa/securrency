using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using SecurrencyTDS.WalletManager.Application.Command;
using Securrency.Domain.Response;
using SecurrencyTDS.WalletManager.Application.Response;

namespace Securrency.Wallet.Controllers
{
    /// <summary>
    /// This controller is for uploading stellar wallet addresses
    /// </summary>
    [ApiController]
   [Authorize]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private readonly IMediator _mediator;

        public WalletController(ILogger<WalletController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }






        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UploadWalletResponse), (int)HttpStatusCode.OK)]
        [Produces("application/json")]

        [HttpPost, Route("uploadwallet")]
        public async Task<IActionResult> UploadWallet([FromBody] UploadWalletAddressesCommand command)
        {

            var result = await _mediator.Send(command);
            if (!result.IsSuccessful)
            {
                return new BadRequestObjectResult(new BaseResponse { Message = result.Message });

            }
            return Ok(result?.ResponseModel);

        }




       

        [HttpGet, Route("exporttransactionhistory.csv")]
        [Produces("application/json")]
        public async Task<IActionResult> WalletTransactionsQueryExport([FromQuery] WalletTransactionsQuery query)
        {

            var result = await _mediator.Send(query);
            if (!result.IsSuccessful)
            {
                return new BadRequestObjectResult(new { Message = result.Message });

            }

            result.ResponseModel.Position = 0; 
            return File(result.ResponseModel, "application/octet-stream", $"TransferHistoryReport_{DateTime.Now.ToString("dd_MMM_yyyy")}.csv");
        }

    }
}
