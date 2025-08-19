using BugTrack.Core.Interfaces;
using BugTracker.Core.Services;
using BugTracker.Infrastructure.Data; // Your DbContext namespace
using BugTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IBugRepository, BugRepository>();
builder.Services.AddSingleton<IBugService, BugService>();
builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<BugTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();