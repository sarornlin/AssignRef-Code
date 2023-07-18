using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Sabio.Models.Requests.Stripe;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using Stripe;
using Stripe.FinancialConnections;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/stripe/transfer")]
    [ApiController]
    public class StripeTransferApiController : BaseApiController
    {
        private IStripeTransferService _service = null;
        private IAuthenticationService<int> _authenticationService = null;
        public StripeTransferApiController(IStripeTransferService service, IAuthenticationService<int> authenticationService,
            ILogger<StripeTransferApiController> logger) : base(logger)
        {
            _service = service;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> CreateTransfer(TransferAddRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authenticationService.GetCurrentUserId();
                int result = _service.CreateTransfer(model, userId);

                if(result == 0)
                {
                    code = 404;
                    response = new ErrorResponse("Problem creating transfer.");
                }
                else
                {
                    response =new ItemResponse<int> { Item = result};
                }
            }
            catch(Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
    }
}
