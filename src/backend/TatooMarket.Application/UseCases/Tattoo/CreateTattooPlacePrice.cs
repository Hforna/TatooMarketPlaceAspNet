using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Services.Validator.Tattoo;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Services;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class CreateTattooPlacePrice : ICreateTattooPlacePrice
    {
        private readonly ITattooWriteOnly _tattooWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _userByToken;
        private readonly SqidsEncoder<long> _sqids;
        private readonly IMapper _mapper;
        public CreateTattooPlacePrice(ITattooWriteOnly tattooWrite, IUnitOfWork unitOfWork,
            IGetUserByToken userByToken, SqidsEncoder<long> sqidsEncoder, 
            IMapper mapper)
        {
            _tattooWrite = tattooWrite;
            _unitOfWork = unitOfWork;
            _userByToken = userByToken;
            _sqids = sqidsEncoder;
            _mapper = mapper;
        }

        public async Task<ResponseTattooPlacePrice> Execute(RequestCreateTattooPlacePrice request)
        {
            Validate(request);
            
            var user = await _userByToken.GetUser();

            if (user.StudioId is null)
                throw new StudioException(ResourceExceptMessages.USER_WITHOUT_STUDIO);

            var tattooPrice = _mapper.Map<TattooPlacePriceEntity>(request);
            tattooPrice.StudioId = (long)user.StudioId;

            await _tattooWrite.AddTattooPlacePrice(tattooPrice);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseTattooPlacePrice>(tattooPrice);
            response.StudioId = _sqids.Encode(tattooPrice.StudioId);
            response.Id = _sqids.Encode(tattooPrice.Id);

            return response;
        }

        private void Validate(RequestCreateTattooPlacePrice request)
        {
            var validator = new CreateTattooPlacePriceValidator();
            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new TattooException(errorMessages);
            }
        }
    }
}
