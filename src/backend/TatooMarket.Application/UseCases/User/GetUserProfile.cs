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

        public GetUserProfile(IMapper mapper, IUserWriteRepository userWrite, IGetUserByToken userByToken, 
            ITattooReadOnly tattooRead, SqidsEncoder<long> sqidsEncoder)
        {
            _mapper = mapper;
            _userWrite = userWrite;
            _userByToken = userByToken;
            _tattooRead = tattooRead;
            _sqidsEncoder = sqidsEncoder;
        }

        public async Task<ResponseGetUserProfile> Execute()
        {
            var user = await _userByToken.GetUser();

            if (user is null)
                throw new UserException("User doesn't exists or is not logged");

            var response = _mapper.Map<ResponseGetUserProfile>(user);

            if(user.Studio is not null)
                response.UserStudio.RecentTattoss = _mapper.Map<IList<ResponseShortTatto>>(user.Studio.StudioTattoss.Take(5).OrderBy(d => d.CreatedOn).ToList());

            response.UserStudio.OwnerId = _sqidsEncoder.Encode(user.Id);

            return response;
        }
    }
}
