using Azure.Messaging.ServiceBus;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Infrastructure.Azure;

namespace TatooMarket.Api.BackgroundServices
{
    public class DeleteService : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;

        public DeleteService(DeleteProcessor processor, IServiceProvider serviceProvider)
        {
            _processor = processor.GetProcessor();
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;

            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();

            var messageFrom = args.Message.ApplicationProperties.TryGetValue("MessageType", out var type) ? type.ToString() : null;

            var scope = _serviceProvider.CreateScope();
            
            if(messageFrom == "DeleteUser")
            {
                var useCase = scope.ServiceProvider.GetRequiredService<IDeleteUser>();

                var userIdentifier = Guid.Parse(message);

                await useCase.Execute(userIdentifier);
            }
        }

        private async Task ProcessErrorAsync(ProcessErrorEventArgs args) => await Task.CompletedTask;

        ~DeleteService() => Dispose();

        public override void Dispose()
        {
            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
