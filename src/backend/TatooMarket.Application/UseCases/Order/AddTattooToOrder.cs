using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Order;
using TatooMarket.Communication.Requests.Order;
using TatooMarket.Domain.Entities.Finance;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Orders;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Order
{
    public class AddTattooToOrder : IAddTattooToOrder
    {
        private readonly IOrderWriteOnly _orderWrite;
        private readonly IOrderReadOnly _orderRead;
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly ITattooReadOnly _tattooRead;

        public AddTattooToOrder(IOrderReadOnly orderRead, IOrderWriteOnly orderWrite,
            IGetUserByToken userByToken, IUnitOfWork unitOfWork, 
            SqidsEncoder<long> sqidsEncoder, ITattooReadOnly tattooRead)
        {
            _orderRead = orderRead;
            _orderWrite = orderWrite;
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _sqidsEncoder = sqidsEncoder;
            _tattooRead = tattooRead;
        }


        public async Task Execute(RequestAddTattooToOrder request, ISession session)
        {
            var user = await _userByToken.GetUser();

            var tattooStyleId = _sqidsEncoder.Decode(request.TattooStyleId).Single();
            var tattooPlaceId = _sqidsEncoder.Decode(request.TattooPlaceId).Single();

            var tattooStyle = await _tattooRead.TattooStylePriceById(tattooStyleId);
            var tattooPlace = await _tattooRead.TattooPlacePriceById(tattooPlaceId);

            if (tattooStyle is null || tattooPlace is null)
                throw new TattooException(ResourceExceptMessages.TATTOOPRICE_DOESNT_EXISTS);

            if (user == null)
            {
                var orderItemsString = new List<String>();
                if(session.TryGetValue("order", out var _))
                {
                    var sessionOrderItems = session.GetString("orderItems");

                    orderItemsString = System.Text.Json.JsonSerializer.Deserialize<List<string>>(sessionOrderItems);                                                             
                } else
                {
                    session.SetString("order", "true");
                }
                orderItemsString.Add($"{request.TattooStyleId}, {request.TattooPlaceId}");
                var orderItemsSerializer = JsonConvert.SerializeObject(orderItemsString);

                session.SetString("orderItems", orderItemsSerializer);
            } else
            {
                var customerOrder = await _orderRead.OrderByUser(user) ?? new TatooMarket.Domain.Entities.Finance.Order() { UserId = user.Id };

                var orderItems = new OrderItemEntity() { BodyPlacement = tattooPlaceId, TattooStyle = tattooStyleId, StudioId = tattooStyle.StudioId };
                orderItems.Price = tattooPlace.Price + tattooStyle.Price;

                customerOrder.TotalPrice += orderItems.Price;
                _orderWrite.UpdateOrder(customerOrder);


                if (customerOrder is null)
                {
                    customerOrder.OrderItems.Add(orderItems);
                    customerOrder.TotalPrice = orderItems.Price;

                    await _orderWrite.AddOrder(customerOrder);
                }
                orderItems.OrderId = customerOrder.Id;

                await _orderWrite.AddOrderItem(orderItems);
                await _unitOfWork.Commit();
            }
        }
    }
}
