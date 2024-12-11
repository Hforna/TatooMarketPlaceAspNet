using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Order;
using TatooMarket.Communication.Requests.Order;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Orders;
using TatooMarket.Domain.Repositories.Security.Token;

namespace TatooMarket.Application.UseCases.Order
{
    public class AddTattooToOrder : IAddTattooToOrder
    {
        private readonly IOrderWriteOnly _orderWrite;
        private readonly IOrderReadOnly _orderRead;
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public async Task Execute(RequestAddTattooToOrder request, ISession session)
        {
            var user = await _userByToken.GetUser();

            if (user == null)
            {
                if(session.TryGetValue("order", out var _))
                {
                    session.
                }
            }
        }
    }
}
