using BugTrack.Application.Services;
using BugTrack.Core.Interfaces;
using BugTrack.Infrastructure.Data;
using BugTrack.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IBugRepository, BugRepository>();
builder.Services.AddScoped<IBugService, BugService>();
builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddDbContext<BugTrackerContext>(optionsAction: options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Using In-Memory Database for simplicity

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
