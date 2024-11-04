using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests;
using TatooMarket.Communication.Responses;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Application.Services.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            RequestToEntitie();
            EntitieToResponse();
        }

        private void RequestToEntitie()
        {
            CreateMap<RequestCreateUser, UserEntity>()
                .ForMember(u => u.Password, opt => opt.Ignore());
        }

        private void EntitieToResponse()
        {
            CreateMap<UserEntity, ResponseCreateUser>();
        }
    }
}
