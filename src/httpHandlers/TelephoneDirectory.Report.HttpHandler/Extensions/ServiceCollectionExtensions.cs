using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Report.HttpHandler.Abstractions;
using TelephoneDirectory.Report.HttpHandler.Concretes;

namespace TelephoneDirectory.Report.HttpHandler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterReportHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IReportHttpHandler, ReportHttpHandler>(c =>
            {
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.BaseAddress = new Uri(configuration.GetSection("Settings").GetSection("Url").GetSection("Report").Value);
            });


        }

    }
}
