using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Contact.HttpHandler.Abstractions;
using TelephoneDirectory.Contact.HttpHandler.Concretes;

namespace TelephoneDirectory.Contact.HttpHandler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterContactHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IContactHttpHandler, ContactHttpHandler>(c =>
            {
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.BaseAddress = new Uri(configuration.GetSection("Settings").GetSection("Url").GetSection("Contact").Value);
            });

        
        }
        
    }
}
