using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Common.Middleware;
using TelephoneDirectory.Data.Access.Context;
using TelephoneDirectory.Service.Abstractions;
using TelephoneDirectory.Service.Concretes;
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
builder.Services.AddPersonServices(builder.Configuration);
builder.Services.AddPersonContactServices(builder.Configuration);
#endregion
#region mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new PersonMapperProfile());
    mc.AddProfile(new PersonContactMapperProfile());
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
public partial class Program { }
