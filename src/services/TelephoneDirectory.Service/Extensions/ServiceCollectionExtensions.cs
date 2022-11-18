using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Service.Abstractions;
using TelephoneDirectory.Service.Concretes;

namespace TelephoneDirectory.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonService, PersonService>();
           
        }
        public static void AddPersonContactServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonContactService, PersonContactService>();
        }

        public static void AddReportServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IReportService, ReportService>();
        }
    }
}
