using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Mq.Service.Abstractions;

namespace TelephoneDirectory.Mq.Service.Concretes
{
    public class RabbitConnectionFactory : IRabbitConnectionFactory
    {
        private readonly IConfiguration configuration;
        public RabbitConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory()
            {
                UserName = configuration.GetSection("Settings").GetSection("UserName").Value,
                Password = configuration.GetSection("Settings").GetSection("Password").Value,
                AutomaticRecoveryEnabled = true,
                HostName = configuration.GetSection("Settings").GetSection("HostName").Value,
                Port = Convert.ToInt32(configuration.GetSection("Settings").GetSection("5672").Value),
                DispatchConsumersAsync = true

            };
            return factory.CreateConnection();
        }
    }
}
