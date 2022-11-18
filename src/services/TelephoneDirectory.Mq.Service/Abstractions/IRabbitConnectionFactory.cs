using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Mq.Service.Abstractions
{
    public interface IRabbitConnectionFactory
    {
        IConnection CreateConnection();
    }
}
