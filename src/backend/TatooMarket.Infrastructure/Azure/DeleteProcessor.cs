using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories.Azure;

namespace TatooMarket.Infrastructure.Azure
{
    public class DeleteProcessor
    {
        private readonly ServiceBusProcessor _serviceBus;

        public DeleteProcessor(ServiceBusProcessor serviceBus) => _serviceBus = serviceBus;

        public ServiceBusProcessor GetProcessor() => _serviceBus;
    }
}
