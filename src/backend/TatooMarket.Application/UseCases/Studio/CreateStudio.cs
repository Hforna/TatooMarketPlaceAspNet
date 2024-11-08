using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Extensions;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Requests.Studio;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Domain.Entities.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Studio
{
    public class CreateStudio : ICreateStudio
    {
        private readonly IMapper _mapper;
        private readonly IStudioWriteOnly _studioWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _getUserByToken;
        private readonly IStudioReadOnly _studioRead;
        private readonly SqidsEncoder<long> _sqidsEncoder;

        public CreateStudio(IMapper mapper, IStudioReadOnly studioRead, IStudioWriteOnly studioWrite, 
            IUnitOfWork unitOfWork, IGetUserByToken getUserByToken, SqidsEncoder<long> sqidsEncoder)
        {
            _mapper = mapper;
            _studioRead = studioRead;
            _studioWrite = studioWrite;
            _unitOfWork = unitOfWork;
            _getUserByToken = getUserByToken;
            _sqidsEncoder = sqidsEncoder;
        }

        
        public async Task<ResponseShortStudio> Execute(RequestCreateStudio request)
        {
            if (await _studioRead.StudioNameExists(request.StudioName))
                throw new StudioException(ResourceExceptMessages.STUDIO_NAME_EXISTS);

            var studio = _mapper.Map<TatooMarket.Domain.Entities.Tattoo.Studio>(request);

            if (request.StudioName != null)
            {
                var image = request.ImageStudio.OpenReadStream();

                (bool isValid, string ext) = ImageValidator.Validate(image);

                if (!isValid)
                    throw new StudioException(ResourceExceptMessages.FILE_FORMAT);

                studio.ImageStudio = $"{Guid.NewGuid}{ext}";
            }

            var user = await _getUserByToken.GetUser();
            studio.OwnerId = user.Id;

            _studioWrite.Add(studio);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseShortStudio>(studio);
            response.OwnerId = _sqidsEncoder.Encode(studio.OwnerId);

            return response;
        }
    }
}
