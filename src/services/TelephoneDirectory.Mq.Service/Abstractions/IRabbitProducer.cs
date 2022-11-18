using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Mq.Service.Abstractions
{
    public interface IRabbitProducer
    {
        bool BasicPublish(string queueName, string item, CancellationToken cancellationToken);
        bool QueueDeclare(string queueName, CancellationToken cancellationToken);
    }
}
