using Amazon.Runtime.Internal.Util;
using Google.Apis.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Sabio.Models.Requests.Stripe;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using static Google.Apis.Requests.BatchRequest;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/stripe/orderreceipts")]
    [ApiController]
    public class StripeOrderReceiptApiController : BaseApiController
    {
        private IAuthenticationService<int> _authenticationService = null;
        private IStripeOrderReceiptService _service = null;
        public StripeOrderReceiptApiController(IStripeOrderReceiptService service, IAuthenticationService<int> authenticationService, ILogger<StripeOrderReceiptApiController> logger) : base(logger)
        {
            _service = service;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> CreateOrderReceipt(OrderReceiptAddRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            try 
            {
                int userId = _authenticationService.GetCurrentUserId();
                int result = _service.Create(model, userId);
                if(result == 0)
                {
                    code = 404;
                    response = new ErrorResponse("An error occured.");
                }
                else
                {
                    response = new ItemResponse<int> { Item = result };
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
