using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Mq.Service.Model;

namespace TelephoneDirectory.Mq.Service.Abstractions
{
    public interface IRabbitConsumer
    {
        void BasicConsumer(string queueName, int parallelConsumerCount, Func<RabbitQueueDto, Task> itemReceivedCallback);
    }
}
