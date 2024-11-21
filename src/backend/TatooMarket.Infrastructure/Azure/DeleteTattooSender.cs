using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories.Azure;

namespace TatooMarket.Infrastructure.Azure
{
    public class DeleteTattooSender : IDeleteTattooSender
    {
        private readonly ServiceBusSender _serviceBusSender;

        public DeleteTattooSender(ServiceBusSender serviceBusSender) => _serviceBusSender = serviceBusSender;

        public async Task SendMessage(long id)
        {
            var message = new ServiceBusMessage(id.ToString());
            message.ApplicationProperties["MessageType"] = "DeleteTattoo";

            await _serviceBusSender.SendMessageAsync(message);
        }
    }
}
