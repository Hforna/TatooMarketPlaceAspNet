using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Finance;
using TatooMarket.Communication.Enums;
using TatooMarket.Communication.Responses.Finance;
using TatooMarket.Domain.Repositories.Finance;
using TatooMarket.Domain.Repositories.Orders;
using TatooMarket.Domain.Repositories.Payment;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Finance
{
    public class BuyTattoo : IBuyTattoo
    {
        private readonly IStudioReadOnly _studioRead;
        private readonly IGetUserByToken _userByToken;
        private readonly IStripeService _stripeService;
        private readonly IFinanceReadOnly _financeRead;
        private readonly IOrderReadOnly _orderRead;

        public BuyTattoo(IStudioReadOnly studioRead, IGetUserByToken userByToken, 
            IStripeService stripeService, IFinanceReadOnly financeRead, 
            IOrderReadOnly orderRead)
        {
            _studioRead = studioRead;
            _userByToken = userByToken;
            _stripeService = stripeService;
            _financeRead = financeRead;
            _orderRead = orderRead;
        }

        public async Task<ResponseStripeUrl> Execute()
        {
            var user = await _userByToken.GetUser();

            if (user is null)
                throw new UserException(ResourceExceptMessages.USER_DOESNT_EXISTS);

            var userOrder = await _orderRead.OrderByUser(user);

            if (userOrder is null)
                throw new FinanceException(ResourceExceptMessages.ORDER_DOESNT_EXISTS);

            var stripeSession = await _stripeService.GenerateSession(userOrder);

            return new ResponseStripeUrl() { PaymentUrl = stripeSession.Url };
        }
    }
}
