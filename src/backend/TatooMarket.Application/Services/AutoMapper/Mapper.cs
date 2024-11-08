﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.Studio;
using TatooMarket.Communication.Requests.User;
using TatooMarket.Communication.Responses.Studio;
using TatooMarket.Communication.Responses.Tatto;
using TatooMarket.Communication.Responses.User;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Entities.Tattoo;

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

            CreateMap<RequestCreateStudio, Studio>();
        }

        private void EntitieToResponse()
        {
            CreateMap<UserEntity, ResponseCreateUser>();

            CreateMap<UserEntity, ResponseGetUserProfile>()
                .ForMember(d => d.UserStudio, opt => opt.MapFrom(d => d.Studio));

            CreateMap<Studio, ResponseShortStudio>()
                .ForMember(d => d.RecentTattoss, opt => opt.Ignore())
                .ForMember(d => d.OwnerId, opt => opt.Ignore());

            CreateMap<TattooEntity, ResponseShortTatto>();
        }
    }
}
