using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Extensions;
using TatooMarket.Application.Services.Validator.Tattoo;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class CreateTattoo : ICreateTattoo
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly ITattooWriteOnly _tattooWrite; 
        private readonly IMapper _mapper;
        private readonly IAzureStorageService _azureStorage;

        public CreateTattoo(IGetUserByToken userByToken, IUnitOfWork unitOfWork, 
            SqidsEncoder<long> sqidsEncoder, ITattooWriteOnly tattooWrite, 
            IMapper mapper, IAzureStorageService azureStorage)
        {
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _sqidsEncoder = sqidsEncoder;
            _tattooWrite = tattooWrite;
            _mapper = mapper;
            _azureStorage = azureStorage;
        }

        public async Task<ResponseShortTatto> Execute(RequestCreateTattoo request)
        {
            Validate(request);
            
            var user = await _userByToken.GetUser();

            if (user!.StudioId is null)
                throw new StudioException(ResourceExceptMessages.USER_WITHOUT_STUDIO);

            var tattoo = _mapper.Map<TattooEntity>(request);

            if(request.TattooImage is not null)
            {
                var image = request.TattooImage.OpenReadStream();

                (bool isValid, string ext) = ImageValidator.Validate(image);

                if (!isValid)
                    throw new TattooException(ResourceExceptMessages.FILE_FORMAT);

                tattoo.TattooImage = $"{Guid.NewGuid}{ext}";

                //await _azureStorage.UploadUser(user, image, tattoo.TattooImage);
            }

            tattoo.StudioId = (long)user.StudioId;
            tattoo.CustomerId = string.IsNullOrEmpty(request.CustomerId) == false ? _sqidsEncoder.Decode(request.CustomerId).Single() : null;

            await _tattooWrite.Add(tattoo);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseShortTatto>(tattoo);
            response.CustomerId = request.CustomerId;
            response.StudioId = _sqidsEncoder.Encode(tattoo.StudioId);

            return response;
        }

        public void Validate(RequestCreateTattoo request)
        {
            var validator = new CreateTattooValidator();
            var result = validator.Validate(request);
            
            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new TattooException(errorMessages);
            }
        }
    }
}
