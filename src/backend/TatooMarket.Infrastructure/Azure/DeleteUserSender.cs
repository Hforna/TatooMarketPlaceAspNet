using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Repositories.Azure;

namespace TatooMarket.Infrastructure.Azure
{
    public class DeleteUserSender : IDeleteUserSender
    {
        private readonly ServiceBusSender _busSender;

        public DeleteUserSender(ServiceBusSender busSender) => _busSender = busSender;

        public async Task SendMessage(UserEntity user)
        {
            var userIdentifier = user.UserIdentifier.ToString();

            var message = new ServiceBusMessage(userIdentifier);
            message.ApplicationProperties["MessageType"] = "DeleteUser";

            await _busSender.SendMessageAsync(message);
        }
    }
}
