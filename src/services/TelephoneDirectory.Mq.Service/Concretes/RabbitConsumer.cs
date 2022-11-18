using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Mq.Service.Abstractions;
using TelephoneDirectory.Mq.Service.Model;

namespace TelephoneDirectory.Mq.Service.Concretes
{
    public class RabbitConsumer : IRabbitConsumer
    {
        private readonly object currentConnectionLock = new object();
        private IConnection currentConnection;
        private readonly IRabbitConnectionFactory connectionFactory;      


        public RabbitConsumer(IRabbitConnectionFactory connectionFactory)
        {          
            this.connectionFactory = connectionFactory;
        }

        public void BasicConsumer(string queueName, int parallelConsumerCount, Func<RabbitQueueDto, Task> itemReceivedCallback)
        {
            var conn = GetConnection();
            var channel = conn.CreateModel();
            channel.BasicQos(0, 1, false);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var processItem = Utf8Json.JsonSerializer.Deserialize<RabbitQueueDto>(body);
                    Console.WriteLine($"{DateTime.Now.ToString()}--BasicConsumer--{queueName}");
                    await itemReceivedCallback(processItem);

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception exception)
                {
                    channel.BasicReject(ea.DeliveryTag, false);
                    await Console.Error.WriteAsync(exception.ToString());
                }
            };

            channel.BasicConsume(queueName, false, consumer);
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
