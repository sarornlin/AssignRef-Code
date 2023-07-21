    [Route("api/stripe/balance")]
    [ApiController]
    public class StripeBalanceApiController : BaseApiController
    {
        private IStripeBalanceService _service = null;
        public StripeBalanceApiController(IStripeBalanceService service,
            ILogger<StripeBalanceApiController> logger) : base(logger) 
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ItemResponse<Balance>> Get()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Balance balance = _service.GetBalance();
                if(balance == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource Not Found");
                }
                else
                {
                    response = new ItemResponse<Balance> { Item= balance };
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
