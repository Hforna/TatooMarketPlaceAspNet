using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Services.Validator.Tattoo;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Communication.Responses.Tattoo;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class CreateTattooStylePrice : ICreateTattooStylePrice
    {
        private readonly IGetUserByToken _userByToken;
        private readonly ITattooWriteOnly _tattooWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly IMapper _mapper;

        public CreateTattooStylePrice(IGetUserByToken userByToken, ITattooWriteOnly tattooWrite, 
            IUnitOfWork unitOfWork, SqidsEncoder<long> sqidsEncoder, 
            IMapper mapper)
        {
            _userByToken = userByToken;
            _tattooWrite = tattooWrite;
            _unitOfWork = unitOfWork;
            _sqidsEncoder = sqidsEncoder;
            _mapper = mapper;
        }

        public async Task<ResponseTattooStylePrice> Execute(RequestCreateTattooStylePrice request)
        {
            Validator(request);

            var user = await _userByToken.GetUser();

            if (user.Studio is null)
                throw new StudioException(ResourceExceptMessages.USER_WITHOUT_STUDIO);

            var tattoo = _mapper.Map<TattooStylePriceEntity>(request);
            tattoo.StudioId = (long)user.StudioId!;

            await _tattooWrite.AddTattooStylePrice(tattoo);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseTattooStylePrice>(tattoo);
            response.Id = _sqidsEncoder.Encode(tattoo.Id);
            response.StudioId = _sqidsEncoder.Encode(tattoo.StudioId);

            return response;
        }

        private void Validator(RequestCreateTattooStylePrice request)
        {
            var validator = new CreateTattooStylePriceValidator();
            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new TattooException(errorMessages);
            }
        }
    }
}
