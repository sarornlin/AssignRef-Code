    [Route("api/checkouts")]
    [ApiController]
    public class CheckoutSessionApiController : BaseApiController
    {

        private ICheckoutSessionService _service = null;
        private IAuthenticationService<int> _authService = null;

        public CheckoutSessionApiController (ICheckoutSessionService service
            , ILogger<CheckoutSessionApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> CreateSession(SessionAddRequest newSession)
        {
            ObjectResult result = null;

            try
            {
                string sessionId = _service.AddSession(newSession);
                ItemResponse<string> response = new ItemResponse<string>() { Item = sessionId };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                ErrorResponse response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, response);
            }
            return result;

        }

        [HttpGet("order/success")]
        public ActionResult<ItemResponse<Session>> GetSession(string sessionId)
        {
            BaseResponse response = null;
            var code = 200;
            try 
            {
                Session session = _service.GetSession(sessionId);
                if (session == null)
                {
                    code = 404;
                    response = new ErrorResponse("Resource not found");
                }
                else
                {
                    response = new ItemResponse<Session>() { Item = session };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }
        
        [HttpGet("order/lineitems")]
        public ActionResult<ItemsResponse<LineItem>> GetLineItems(string session_id) {

            BaseResponse response = null;
            var code = 200;
            try
            {
                StripeList<LineItem> lineItems = _service.GetLineItems(session_id);
                if (lineItems == null)
                {
                    code = 404;
                    response = new ErrorResponse("Resources not found");

                }
                else
                {
                    response = new ItemsResponse<LineItem>() { Items = lineItems.Data };

                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
                }
        
    }
