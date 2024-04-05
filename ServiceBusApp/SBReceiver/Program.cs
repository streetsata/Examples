using Azure.Core.Extensions;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using SBShared.Models;
using System.Text;
using System.Text.Json;

namespace SBReceiver
{
    public class Program
    {
        const string connectionString = "Endpoint=sb://sbstreetsata.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0GihOmlqyCGq1t8uLI7r3t9q3Sz4Thi4p+ASbMpTcyI=";
        const string queueName = "personqueue";

        protected Program() { }

        static async Task Main(string[] args)
        {
            var serviceBusSender = GetQueueClient(queueName);
            var receivedMessage = await serviceBusSender.ReceiveMessageAsync().ConfigureAwait(true);

            var jsonString = Encoding.UTF8.GetString(receivedMessage.Body);

            PersonModel? person = JsonSerializer.Deserialize<PersonModel>(jsonString) ?? null;

            Console.WriteLine($"Person Received: {person.FirstName}, {person.LastName}");

        }

        public static ServiceBusReceiver GetQueueClient(string queueName)
        {
            var serviceBusClient = new ServiceBusClient(connectionString);
            
            return serviceBusClient.CreateReceiver(queueName);
        }
    }
}
