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
using TatooMarket.Domain.Repositories.Tattoo;
using Microsoft.Extensions.Configuration;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;
using TatooMarket.Domain.Repositories.Orders;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Services;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Infrastructure.Payment
{
    public class StripeService : IStripeService
    {
        private readonly ITattooReadOnly _tattooRead;
        private readonly string _secretKey;
        private readonly IUserReadRepository _userRead;
        private readonly IOrderReadOnly _orderRead;
        private readonly IOrderWriteOnly _orderWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendEmailService _sendEmail;

        public StripeService(ITattooReadOnly tattooRead, IConfiguration configuration, 
            IUserReadRepository userRead, IOrderReadOnly orderRead,
            IOrderWriteOnly orderWrite, IUnitOfWork unitOfWork, ISendEmailService sendEmail)
        {
            _tattooRead = tattooRead;
            _secretKey = configuration.GetValue<string>("payment:stripe:secretKey")!;
            _userRead = userRead;
            _orderRead = orderRead;
            _unitOfWork = unitOfWork;
            _orderWrite = orderWrite;
            _unitOfWork = unitOfWork;
            _sendEmail = sendEmail;
        }

        public async Task<Session> GenerateSession(Order order)
        {
            var domain = "https://localhost:8080/payment/";
            var session = new SessionCreateOptions()
            {
                SuccessUrl = domain,
                CancelUrl = domain,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };

            var bodyPlacement = await _tattooRead.TattooPlacePriceById(order.OrderItems.FirstOrDefault().BodyPlacementId);

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
                        Currency = bodyPlacement.CurrencyType.ToString(),
                        UnitAmount = (long?)(orderItem.Price * 100)
                    },
                    Quantity = 1
                };
                session.LineItems.Add(sessionLineItem);
            }

            var sessionService = new SessionService();
            var createService = await sessionService.CreateAsync(session);

            return createService;
        }

        public async Task WebHookService(string jsonBody, string stripeSignature)
        {
            var stripeEvent = EventUtility.ConstructEvent(jsonBody, stripeSignature, _secretKey, throwOnApiVersionMismatch: false);

            var stripeObject = stripeEvent.Data.Object as PaymentIntent;
            var customerEmail = stripeObject.Customer.Email;
            var user = await _userRead.UserByEmail(customerEmail);

            if (user == null)
                throw new UserException(ResourceExceptMessages.USER_DOESNT_EXISTS);

            var userOrder = await _orderRead.OrderByUser(user);

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    var orderItemsDict = new Dictionary<Studio, List<List<String>>>();
                    foreach(var orderItem in userOrder.OrderItems)
                    {
                        var tattoosType = new List<string>() { orderItem.TattooStyle.ToString(), orderItem.BodyPlacement.ToString()};
                        if (orderItemsDict.ContainsKey(orderItem.Studio))
                        {
                            orderItemsDict[orderItem.Studio].Add(tattoosType);
                        } else
                        {
                            orderItemsDict[orderItem.Studio] = new List<List<string>>() { tattoosType };
                        }
                    }

                    foreach(var studio in orderItemsDict)
                    {
                        await _sendEmail.SendEmail(studio.Key.Owner.Email, 
                            $"customer: {user}\nTattoo style: {studio.Value[0]}, Body placement: {studio.Value[1]}", "You got a new purchase", 
                            studio.Key.Owner.UserName);
                    }


                    userOrder.Active = false;
                    _orderWrite.UpdateOrder(userOrder);
                    await _unitOfWork.Commit();

                    break;
            }
        }
    }
}
