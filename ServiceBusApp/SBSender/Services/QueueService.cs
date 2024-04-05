using Azure.Messaging.ServiceBus;
using System.Text;
using System.Text.Json;

namespace SBSender.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration config;

        public QueueService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
        {
            var queueClient = new ServiceBusClient(config.GetConnectionString("AzureServiceBus"));
            var messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

            await queueClient.CreateSender(queueName).SendMessageAsync(message);
        }
    }
}
