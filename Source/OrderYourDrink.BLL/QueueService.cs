using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderYourDrink.BLL.Models;
using System.Text;
using System.Text.Json;

namespace ChoseYourDrink.BLL
{
    public interface IQueueService
    {
        IDictionary<string, string> MessageQueues { get; }
        Task<string> SendMessageAsync<T>(T message, string queueName);
    }

    public class QueueService : IQueueService
    {
        private readonly ILogger<QueueService> _logger;

        private readonly ServiceBusSettings _serviceBusSettings;
        protected ServiceBusSettings ServiceBusSettings => _serviceBusSettings;

        public IDictionary<string, string> MessageQueues=>_serviceBusSettings.MessageQueues;

        public QueueService(ILogger<QueueService> logger, ServiceBusSettings serviceBusSettings)
        {
            _logger = logger;
            _serviceBusSettings = serviceBusSettings;
        }

        public async Task<string> SendMessageAsync<T>(T message, string queueName)
        {
            _logger.LogTrace("Sending message to queue {queueName}: {@message}", queueName, message);

            QueueClient queueClient = new(ServiceBusSettings.ConnectionString, queueName);

            string messageBody = JsonSerializer.Serialize(message);
            Message queueMessage = new(Encoding.UTF8.GetBytes(messageBody));

            await queueClient.SendAsync(queueMessage);
            return queueMessage.MessageId;
        }
    }
}
