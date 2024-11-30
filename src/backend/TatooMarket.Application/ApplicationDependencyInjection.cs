using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Address;
using TatooMarket.Application.UseCases.Finance;
using TatooMarket.Application.UseCases.Login;
using TatooMarket.Application.UseCases.Repositories.Address;
using TatooMarket.Application.UseCases.Repositories.Finance;
using TatooMarket.Application.UseCases.Repositories.Login;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Application.UseCases.Studio;
using TatooMarket.Application.UseCases.Tattoo;
using TatooMarket.Application.UseCases.User;

namespace TatooMarket.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddSqIds(services, configuration);
            AddRepositories(services, configuration);
            AddMapper(services);
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICreateUser, CreateUser>();
            services.AddScoped<ILoginByApplication, LoginByApplication>();
            services.AddScoped<IGetUserProfile, GetUserProfile>();
            services.AddScoped<ICreateStudio, CreateStudio>();
            services.AddScoped<IUpdateUser, UpdateUser>();
            services.AddScoped<IGetStudios, GetStudios>();
            services.AddScoped<IDeleteUserRequest, DeleteUserRequest>();
            services.AddScoped<IDeleteUser, DeleteUser>();
            services.AddScoped<ICreateTattoo, CreateTattoo>();
            services.AddScoped<ICreateTattooReview, CreateTattooReview>();
            services.AddScoped<IDeleteTattooReview, DeleteTattooReview>();
            services.AddScoped<IGetStudioTattoos, GetStudioTattoos>();
            services.AddScoped<IGetWeeksTattoos, GetWeeksTattoos>();
            services.AddScoped<ICreateTattooPrice, CreateTattooPrice>();
            services.AddScoped<IUpdateTattooPrice, UpdateTattooPrice>();
            services.AddScoped<IStudioPriceCatalog, StudioPriceCatalog>();
            services.AddScoped<IDeleteTattooRequest, DeleteTattooRequest>();
            services.AddScoped<IDeleteTattoo, DeleteTattoo>();
            services.AddScoped<ICreateAddress, CreateAddress>();
            services.AddScoped<IGetStudio, GetStudio>();
            services.AddScoped<ICreateFinanceAccount, CreateFinanceAccount>();
        }

        private static void AddSqIds(IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                Alphabet = configuration.GetSection("sqIds:Alphabet").Value!,
                MinLength = int.Parse(configuration.GetSection("sqIds:MinLength").Value!)
            });

            services.AddSingleton(sqids);
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
