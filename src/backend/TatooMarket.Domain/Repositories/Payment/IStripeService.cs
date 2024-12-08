using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;

namespace TatooMarket.Domain.Repositories.Payment
{
    public interface IStripeService
    {
        public Task<Session> GenerateSession(Order order);
    }
}
