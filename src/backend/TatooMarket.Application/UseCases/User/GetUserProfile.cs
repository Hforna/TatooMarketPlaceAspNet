using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Communication.Responses.User;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.User
{
    public class GetUserProfile : IGetUserProfile
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteRepository _userWrite;
        private readonly IGetUserByToken _userByToken;
        private readonly ITattooReadOnly _tattooRead;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly IAzureStorageService _storageService;

        public GetUserProfile(IMapper mapper, IUserWriteRepository userWrite, IGetUserByToken userByToken, 
            ITattooReadOnly tattooRead, SqidsEncoder<long> sqidsEncoder, IAzureStorageService storageService)
        {
            _mapper = mapper;
            _userWrite = userWrite;
            _userByToken = userByToken;
            _tattooRead = tattooRead;
            _sqidsEncoder = sqidsEncoder;
            _storageService = storageService;
        }

        public async Task<ResponseGetUserProfile> Execute()
        {
             var user = await _userByToken.GetUser();

            if (user is null)
                throw new UserException("User doesn't exists or is not logged");

            var userTattoos = await _tattooRead.TattoosFromStudio(user.Studio);

            var response = _mapper.Map<ResponseGetUserProfile>(user);

            if(user.Studio is not null)
            {
                if(userTattoos is not null)
                {
                    var studioTattoss = userTattoos.Take(5).OrderBy(d => d.CreatedOn).Select(async tattoo =>
                    {
                        var response = _mapper.Map<ResponseShortTatto>(tattoo);

                        response.TattoImage = await _storageService.GetImage(tattoo.Id.ToString(), tattoo.TattooImage);
                        response.StudioId = _sqidsEncoder.Encode(tattoo.StudioId);
                        response.CustomerId = tattoo.CustomerId != null ? _sqidsEncoder.Encode((long)tattoo.CustomerId) : "";
                        response.Id = _sqidsEncoder.Encode(tattoo.Id);

                        return response;
                    }).ToList();

                    var studioTattoosTask = await Task.WhenAll(studioTattoss);

                    response.UserStudio.RecentTattoss = _mapper.Map<IList<ResponseShortTatto>>(studioTattoosTask);
                }

                response.UserStudio.OwnerId = _sqidsEncoder.Encode(user.Id);
            }

            return response;
        }
    }
}
