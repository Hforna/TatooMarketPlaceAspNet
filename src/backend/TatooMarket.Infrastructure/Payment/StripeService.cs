using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stripe;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Finance;
using TatooMarket.Domain.Entities.Identity;
using Stripe.Checkout;
using TatooMarket.Domain.Repositories.Payment;

namespace TatooMarket.Infrastructure.Payment
{
    public class StripeService : IStripeService
    {
        public async Task<Session> GenerateSession(Order order)
        {
            var domain = "localhost:8080/payment/";
            var session = new SessionCreateOptions()
            {
                SuccessUrl = domain += "success",
                CancelUrl = domain + "cancel",
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };

            foreach(var orderItem in order.OrderItems)
            {
                var sessionLineItem =  new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = orderItem.ToString(),
                        },
                        Currency = "brl",
                        UnitAmountDecimal = orderItem.Price
                    },
                };
                session.LineItems.Add(sessionLineItem);
            }

            var sessionService = new SessionService();
            var createService = await sessionService.CreateAsync(session);

            return createService;
        }
    }
}
;