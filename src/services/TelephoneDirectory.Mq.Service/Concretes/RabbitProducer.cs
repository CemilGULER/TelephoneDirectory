using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Mq.Service.Abstractions;
using TelephoneDirectory.Mq.Service.Model;

namespace TelephoneDirectory.Mq.Service.Concretes
{
    public class RabbitProducer : IRabbitProducer
    {
        private IConnection currentConnection;
        private readonly object currentConnectionLock = new object();
        private readonly IRabbitConnectionFactory connectionFactory;
       

        public RabbitProducer(IRabbitConnectionFactory connectionFactory)
        {           
            this.connectionFactory = connectionFactory;
        }
        public bool QueueDeclare(string queueName, CancellationToken cancellationToken)
        {
            using var channel = GetChannel();
            if (!channel.IsOpen)
            {
                throw new InvalidOperationException(channel.CloseReason.ReplyText);
            }
            channel.QueueDeclare(queueName, true, false, false, null);
            return true;
        }

        public bool BasicPublish(string queueName, string item, CancellationToken cancellationToken)
        {
            using var channel = GetChannel();
            if (!channel.IsOpen)
            {
                throw new InvalidOperationException(channel.CloseReason.ReplyText);
            }
            var rabbitQueueDto = new RabbitQueueDto();
            rabbitQueueDto.Data = item;
            var body = Utf8Json.JsonSerializer.Serialize(rabbitQueueDto);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.BasicPublish("", queueName, null, body);
            Console.WriteLine($"{DateTime.Now.ToString()}--BasicPublish--{queueName}");
            return true;
        }

        private IModel GetChannel()
        {
            return CreateNewChannel();
        }

        private IModel CreateNewChannel()
        {
            return GetConnection().CreateModel();
        }

        private IConnection GetConnection()
        {
            if (currentConnection != null && currentConnection.IsOpen)
            {
                return currentConnection;
            }
            lock (currentConnectionLock)
            {
                if (currentConnection == null || !currentConnection.IsOpen)
                {
                    currentConnection ??= CreateNewConnection();
                }
                return currentConnection;
            }
        }

        private IConnection CreateNewConnection()
        {
            return connectionFactory.CreateConnection();
        }
    }
}
