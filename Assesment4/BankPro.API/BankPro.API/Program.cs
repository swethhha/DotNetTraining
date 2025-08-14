using BankPro.Application.Services;
using BankPro.Core.Interfaces;
using BankPro.Infrastructure.Repositories;
using BankPro.Application.Mapping;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper from Application layer
builder.Services.AddAutoMapper(typeof(BankProProfile));

// Register In-Memory Repositories as Singleton
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();

// Register Services as Scoped
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
