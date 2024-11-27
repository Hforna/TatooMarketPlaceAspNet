using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Sessions;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Studio
{
    public class GetStudio : IGetStudio
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IStudioReadOnly _studioRead;
        private readonly SqidsEncoder<long> _sqids;
        private readonly IMapper _mapper;
        private readonly IStudioWriteOnly _studioWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetCustomerSession _customerSession;
        private readonly IAzureStorageService _storageService;

        public GetStudio(IGetUserByToken userByToken, IStudioReadOnly studioRead, 
            SqidsEncoder<long> sqids, IMapper mapper, 
            IStudioWriteOnly studioWrite, IUnitOfWork unitOfWork, 
            IGetCustomerSession customerSession, IAzureStorageService storageService)
        {
            _userByToken = userByToken;
            _studioRead = studioRead;
            _sqids = sqids;
            _mapper = mapper;
            _studioWrite = studioWrite;
            _unitOfWork = unitOfWork;
            _customerSession = customerSession;
            _storageService = storageService;
            _userByToken = userByToken;
        }

        public async Task<ResponseFullStudio> Execute(long id)
        {
            var studio = await _studioRead.StudioById(id, true);

            if (studio is null)
                throw new StudioException(ResourceExceptMessages.STUDIO_DOESNT_EXISTS);

            var thereIsSession = _customerSession.ThereIsSession("SessionGetStudio");

            _customerSession.GetSessionIdentifier("SessionGetStudio");

            {
                studio.NumberVisits += 1;

                _studioWrite.Update(studio);
                await _unitOfWork.Commit();
            }

            _studioWrite.Update(studio);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseFullStudio>(studio);

            var tattoosList = studio.StudioTattoss.OrderByDescending(d => d.CreatedOn)
                .Take(5)
                .Select(async tattoo =>
                {
                    var response = _mapper.Map<ResponseShortTatto>(tattoo);

                    response.StudioId = _sqids.Encode(tattoo.StudioId);
                    response.Id = _sqids.Encode(tattoo.Id);
                    response.CustomerId = tattoo.CustomerId is not null ? _sqids.Encode((long)tattoo.CustomerId) : "";
                    response.TattoImage = await _storageService.GetImage(studio.Owner.UserIdentifier.ToString(), tattoo.TattooImage);

                    return response;
                }).ToList();

            var taskTattoos = await Task.WhenAll(tattoosList);

            response.OwnerId = _sqids.Encode(studio.OwnerId);
            response.RecentTattoos = taskTattoos;

            if (String.IsNullOrEmpty(studio.ImageStudio) == false)
                response.StudioLogo = await _storageService.GetImage(studio.Owner.UserIdentifier.ToString(), studio.ImageStudio);

            return response;
        }
    }
}
