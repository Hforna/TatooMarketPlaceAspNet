using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Communication.Responses.User;
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

        public GetUserProfile(IMapper mapper, IUserWriteRepository userWrite, IGetUserByToken userByToken, ITattooReadOnly tattooRead)
        {
            _mapper = mapper;
            _userWrite = userWrite;
            _userByToken = userByToken;
            _tattooRead = tattooRead;
        }

        public async Task<ResponseGetUserProfile> Execute()
        {
            var user = await _userByToken.GetUser();

            if (user is null)
                throw new UserException("User doesn't exists or is not logged");

            var response = _mapper.Map<ResponseGetUserProfile>(user);

            if(user.Studio is not null)
                response.UserStudio.RecentTattoss = _mapper.Map<IList<ResponseShortTatto>>(_tattooRead.GetRecentTattoss(user.Studio));

            return response;
        }
    }
}
