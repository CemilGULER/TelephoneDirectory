using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelephoneDirectory.Contact.HttpHandler.Abstractions;
using TelephoneDirectory.Contact.HttpHandler.Extensions;
using TelephoneDirectory.Mq.Service.Abstractions;
using TelephoneDirectory.Mq.Service.Extensions;
using TelephoneDirectory.Report.HttpHandler.Abstractions;
using TelephoneDirectory.Report.HttpHandler.Extensions;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (env == null)
    env = "Development";
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) => { config.AddJsonFile($"appsettings.{env}.json"); })
    .ConfigureServices((context, services) =>
    {
        services.AddRabbitServices(context.Configuration);
        services.RegisterContactHttpClient(context.Configuration);
        services.RegisterReportHttpClient(context.Configuration);
    }).UseConsoleLifetime().Build();
await host.StartAsync();
using var scope = host.Services.CreateScope();
var rabbitConsumer = scope.ServiceProvider.GetService<IRabbitConsumer>();
var contactHttpHandler = scope.ServiceProvider.GetService<IContactHttpHandler>();
var reportHttpHandler = scope.ServiceProvider.GetService<IReportHttpHandler>();
rabbitConsumer.BasicConsumer("report-request", 1, async queueItem =>
{
    var data = new Guid(queueItem.Data);
    var repostData =await  contactHttpHandler.LocationDetail(default);
    //TODO: Burada repostData ile gelen veri Excel'e dönüşüm işlemi yaplarak S3'e yüklenme işlemi yapılacak 
    await reportHttpHandler.CompletedReport(
        new TelephoneDirectory.Report.Contracts.Dto.CompletedReportRequest
        {
            Id = data,
            ReportPath = "/AWSDOC-EXAMPLE-BUCKET/report-request",
            ReportFilName = $"{queueItem.Data}.xlsx"

        },default);


});
await host.WaitForShutdownAsync();