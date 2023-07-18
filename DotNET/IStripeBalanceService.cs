using Stripe;

namespace Sabio.Services.Interfaces
{
    public interface IStripeBalanceService
    {
        Balance GetBalance();
    }
}