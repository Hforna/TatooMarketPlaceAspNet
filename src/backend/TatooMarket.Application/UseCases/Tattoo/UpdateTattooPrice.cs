using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class UpdateTattooPrice : IUpdateTattooPrice
    {
        private readonly ITattooReadOnly _tattooRead;
        private readonly ITattooWriteOnly _tattooWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _userByToken;
        private readonly IMapper _mapper;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public UpdateTattooPrice(ITattooReadOnly tattooRead, ITattooWriteOnly tattooWrite,
            IUnitOfWork unitOfWork, IGetUserByToken userByToken,
            IMapper mapper, SqidsEncoder<long> sqidsEncoder)
        {
            _tattooRead = tattooRead;
            _unitOfWork = unitOfWork;
            _userByToken = userByToken;
            _mapper = mapper;
            _sqidsEncoder = sqidsEncoder;
            _tattooWrite = tattooWrite;
        }

        public async Task<ResponseTattooPlacePrice> Execute(RequestUpdateTattooPrice request, long id)
        {
            var user = await _userByToken.GetUser();

            if (user.StudioId is null)
                throw new StudioException(ResourceExceptMessages.USER_WITHOUT_STUDIO);

            var tattooPrice = await _tattooRead.TattooPriceById(id);

            if (tattooPrice is null)
                throw new TattooException(ResourceExceptMessages.TATTOOPRICE_DOESNT_EXISTS);

            tattooPrice.Price = request.Price;

            _tattooWrite.UpdateTattooPrice(tattooPrice);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseTattooPlacePrice>(tattooPrice);
            response.StudioId = _sqidsEncoder.Encode(tattooPrice.StudioId);
            response.Id = _sqidsEncoder.Encode(tattooPrice.Id);

            return response;
        }
    }
}
