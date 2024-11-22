using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Services.Validator;
using TatooMarket.Application.UseCases.Repositories.Address;
using TatooMarket.Communication.Requests.Address;
using TatooMarket.Communication.Responses.Address;
using TatooMarket.Domain.Entities;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Address;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Services;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Address
{
    public class CreateAddress : ICreateAddress
    {
        private readonly IAddressWriteOnly _addressWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _userByToken;
        private readonly IPostalCodeInfosService _postalService;
        private readonly SqidsEncoder<long> _sqids;
        private readonly IMapper _mapper;

        public CreateAddress(IAddressWriteOnly addressWrite, IUnitOfWork unitOfWork, IGetUserByToken userByToken, IPostalCodeInfosService postalService, SqidsEncoder<long> sqids, IMapper mapper)
        {
            _addressWrite = addressWrite;
            _unitOfWork = unitOfWork;
            _userByToken = userByToken;
            _postalService = postalService;
            _sqids = sqids;
            _mapper = mapper;
        }

        public async Task<ResponseAddress> Execute(RequestCreateAddress request)
        {
            Validate(request);

            var user = await _userByToken.GetUser();

            if (user.StudioId is null)
                throw new StudioException(ResourceExceptMessages.STUDIO_DOESNT_EXISTS);

            try
            {
              var getCeps = await _postalService.GetPostalCodeInfos(request.PostalCode);
            } 
            catch(HttpRequestException e)
            {
                throw new HttpRequestException(ResourceExceptMessages.INVALID_POSTALCODE);
            }

            var address = _mapper.Map<StudioAddress>(request);
            address.Country = "Brazil";
            address.StudioId = (long)user.StudioId;

            await _addressWrite.Add(address);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseAddress>(address);
            response.StudioId = _sqids.Encode(address.StudioId);

            return response;
        }

        private static void Validate(RequestCreateAddress request)
        {
            var validator = new CreateAddressValidator();
            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new StudioException(errorMessages);
            }
        }
    }
}
