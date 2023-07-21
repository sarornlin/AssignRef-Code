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
