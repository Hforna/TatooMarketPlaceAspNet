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
using TatooMarket.Domain.Repositories.User;
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
        private readonly IUserWriteRepository _userWrite;

        public CreateStudio(IMapper mapper, IStudioReadOnly studioRead, IStudioWriteOnly studioWrite, 
            IUnitOfWork unitOfWork, IGetUserByToken getUserByToken, 
            SqidsEncoder<long> sqidsEncoder, IUserWriteRepository userWrite)
        {
            _mapper = mapper;
            _studioRead = studioRead;
            _studioWrite = studioWrite;
            _unitOfWork = unitOfWork;
            _getUserByToken = getUserByToken;
            _sqidsEncoder = sqidsEncoder;
            _userWrite = userWrite;
        }

        
        public async Task<ResponseShortStudio> Execute(RequestCreateStudio request)
        {
            if (await _studioRead.StudioNameExists(request.StudioName))
                throw new StudioException(ResourceExceptMessages.STUDIO_NAME_EXISTS);

            var user = await _getUserByToken.GetUser();

            if (await _studioRead.StudioByOwner(user!) != null)
                throw new StudioException(ResourceExceptMessages.USER_ALREADY_HAS_STUDIO);

            var studio = _mapper.Map<TatooMarket.Domain.Entities.Tattoo.Studio>(request);

            if (request.ImageStudio != null)
            {
                var image = request.ImageStudio.OpenReadStream();

                (bool isValid, string ext) = ImageValidator.Validate(image);

                if (!isValid)
                    throw new StudioException(ResourceExceptMessages.FILE_FORMAT);

                studio.ImageStudio = $"{Guid.NewGuid}{ext}";
            }

            studio.OwnerId = user.Id;


            _studioWrite.Add(studio);
            await _unitOfWork.Commit();

            user.StudioId = studio.Id;

            _userWrite.Update(user);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseShortStudio>(studio);

            response.OwnerId = _sqidsEncoder.Encode(studio.OwnerId);

            return response;
        }
    }
}
