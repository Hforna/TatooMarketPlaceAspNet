using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Address;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Finance;
using TatooMarket.Domain.Repositories.Review;
using TatooMarket.Domain.Repositories.Security.Cryptography;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Services;
using TatooMarket.Domain.Repositories.StudioRepository;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Infrastructure.Azure;
using TatooMarket.Infrastructure.DataEntity;
using TatooMarket.Infrastructure.Security.Cryptography;
using TatooMarket.Infrastructure.Security.Token;
using TatooMarket.Infrastructure.Services;
using TatooMarket.Domain.Repositories.Payment;
using TatooMarket.Infrastructure.Payment;
using TatooMarket.Domain.Repositories.Orders;

namespace TatooMarket.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddJwtTokenRepositories(services, configuration);
            AddRepositories(services, configuration);
            AddStorageBlob(services, configuration);
            AddServiceBus(services, configuration);
            AddCryptography(services);
            AddApiServices(services, configuration);
            AddEmailSerice(services, configuration);
            AddPaymentsService(services, configuration);
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("sqlserverconnection");
            services.AddDbContext<ProjectDbContext>(opt => opt.UseSqlServer(dbConnection).LogTo
            (Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information
            ).EnableSensitiveDataLogging());
        }

        private static void AddCryptography(IServiceCollection services)
        {
            services.AddSingleton<IPasswordCryptography, BcryptNet>();
        }

        private static void AddJwtTokenRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var signKey = configuration.GetValue<string>("security:token:sign_key")!;

            services.AddScoped<ITokenValidator>(opt => new TokenValidator(signKey));
            services.AddScoped<ITokenGenerator>(d => new GenerateJwtToken(signKey));
        }

        private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //User
            services.AddScoped<IUserReadRepository, UserDbContext>();
            services.AddScoped<IUserWriteRepository, UserDbContext>();
            services.AddScoped<IGetUserByToken, GetUserByToken>();

            //Tattoo
            services.AddScoped<ITattooReadOnly, TattooDbContext>();
            services.AddScoped<ITattooWriteOnly, TattooDbContext>();

            //Review
            services.AddScoped<IReviewReadOnly, ReviewDbContext>();
            services.AddScoped<IReviewWriteOnly, ReviewDbContext>();

            //Studio
            services.AddScoped<IStudioReadOnly, StudioDbContext>();
            services.AddScoped<IStudioWriteOnly, StudioDbContext>();

            //Address
            services.AddScoped<IAddressReadOnly, AddressDbContext>();
            services.AddScoped<IAddressWriteOnly, AddressDbContext>();

            //Finance
            services.AddScoped<IFinanceReadOnly, FinanceDbContext>();
            services.AddScoped<IFinanceWriteOnly, FinanceDbContext>();

            //Order
            services.AddScoped<IOrderReadOnly, OrderDbContext>();
            services.AddScoped<IOrderWriteOnly, OrderDbContext>();
        }

        private static void AddApiServices(IServiceCollection services, IConfiguration configuration)
        {
            var exchangeKey = configuration.GetValue<string>("currencyExchange:apiKey");

            services.AddScoped<ICurrencyExchangeService>(d => new CurrencyExchangeService(exchangeKey!));

            services.AddScoped<IPostalCodeInfosService, PostalCodeInfosService>();
        }

        private static void AddPaymentsService(IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration.GetValue<string>("payment:stripe:secretKey");

            StripeConfiguration.ApiKey = secretKey;

            services.AddScoped<IStripeService, StripeService>();
        }

        private static void AddEmailSerice(IServiceCollection services, IConfiguration configuration)
        {
            var email = configuration.GetValue<string>("email:email")!;
            var password = configuration.GetValue<string>("email:password")!;
            var name = configuration.GetValue<string>("email:name")!;

            services.AddScoped<ISendEmailService>(d => new SendEmailService(email, name, password));
        }

        private static void AddServiceBus(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("serviceBus:azure");

            var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets});

            var userSender = new DeleteUserSender(client.CreateSender("delete"));
            services.AddScoped<IDeleteUserSender>(d => userSender);

            var tattooSender = new DeleteTattooSender(client.CreateSender("delete"));
            services.AddScoped<IDeleteTattooSender>(d => tattooSender);

            var processor = new DeleteProcessor(client.CreateProcessor("delete", new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 1
            }));

            services.AddSingleton(processor);
        }

        private static void AddStorageBlob(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("storageBlob:azure");
            var client = new BlobServiceClient(connectionString);

            services.AddScoped<IAzureStorageService>(e => new AzureStorageService(client));
        }
    }
}
