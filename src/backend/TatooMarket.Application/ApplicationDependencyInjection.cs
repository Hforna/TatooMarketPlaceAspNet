using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Login;
using TatooMarket.Application.UseCases.Repositories.Login;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Application.UseCases.User;

namespace TatooMarket.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services, configuration);
            AddMapper(services);
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICreateUser, CreateUser>();
            services.AddScoped<ILoginByApplication, LoginByApplication>();
            services.AddScoped<IGetUserProfile, GetUserProfile>();
        }

        private static void AddMapper(this IServiceCollection services)
        {
            services.AddScoped(opt =>
                new AutoMapper.MapperConfiguration(x =>
                {
                    x.AddProfile(new TatooMarket.Application.Services.AutoMapper.Mapper());
                }).CreateMapper()
            );
        }
    }
}
