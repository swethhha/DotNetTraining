using EventEase.API.Extensions;
using EventEase.Application.Services;
using EventEase.Core.Interfaces;
using EventEase.Infrastructure.Repositories; 
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IEventService, EventService>();

builder.Services.AddSingleton<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddSingleton<IRegistrationService, RegistrationService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
