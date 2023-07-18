using Microsoft.Extensions.Options;
using Sabio.Data.Providers;
using Sabio.Models.Domain.Stripe;
using Sabio.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class StripeBalanceService : IStripeBalanceService
    {
        private StripeKeys _stripeKeys = null;
        private HostUrl _hostUrl = null;
        private BalanceService _balanceService = null;
        public StripeBalanceService(IOptions<StripeKeys> stripekeys, IOptions<HostUrl> hostUrl)
        {
            _stripeKeys = stripekeys.Value;
            _hostUrl = hostUrl.Value;
            _balanceService = new BalanceService();

            StripeConfiguration.ApiKey = _stripeKeys.SecretKey;
        }

        public Balance GetBalance()
        {
            Balance balance = _balanceService.Get();
            return balance;
        }
    }
}
