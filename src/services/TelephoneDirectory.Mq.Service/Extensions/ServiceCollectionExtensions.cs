using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Mq.Service.Abstractions;
using TelephoneDirectory.Mq.Service.Concretes;

namespace TelephoneDirectory.Mq.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitConnectionFactory, RabbitConnectionFactory>();
            services.AddSingleton<IRabbitConsumer, RabbitConsumer>();
            services.AddSingleton<IRabbitProducer, RabbitProducer>();
        }
    }
}
