using Sabio.Models.Requests.Stripe;
using Stripe;

namespace Sabio.Services.Interfaces
{
    public interface IStripeTransferService
    {
        int CreateTransfer(TransferAddRequest model, int userId);
    }
}