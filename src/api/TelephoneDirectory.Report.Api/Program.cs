using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Common.Middleware;
using TelephoneDirectory.Data.Access.Context;
using TelephoneDirectory.Mq.Service.Extensions;
using TelephoneDirectory.Service.Extensions;
using TelephoneDirectory.Service.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TelephoneDirectoryDbContext>(options =>
            options.UseNpgsql(builder.Configuration["Settings:Database:ConnectionString"]));
#region di
builder.Services.AddRabbitServices(builder.Configuration);
builder.Services.AddReportServices(builder.Configuration);
#endregion


#region mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ReportMapperProfile());
   
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

public partial class ProgramReport { }
