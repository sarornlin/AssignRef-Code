using Sabio.Models.Requests.Stripe;

namespace Sabio.Services.Interfaces
{
    public interface IStripeOrderReceiptService
    {
        int Create(OrderReceiptAddRequest model, int userId);
    }
}