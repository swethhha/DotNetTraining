using BugTrack.API.Extensions;
using BugTrack.Core.Interfaces;
using BugTracker.Core.Services;
using BugTracker.Infrastructure.Data;
using BugTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<IBugRepository, BugRepository>();
builder.Services.AddScoped<IBugService, BugService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtConfig = builder.Configuration.GetSection("Jwt");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireDeveloper", policy => policy.RequireRole("Developer"));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<BugTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ✅ Swagger with JWT Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo
    //{
    //    Title = "BugTracker API",
    //    Version = "v1",
    //    Description = "BugTracker with JWT Authentication"
    //});
    c.SwaggerDoc("bugs", new OpenApiInfo { Title = "Bugs API", Version = "v1" });
    c.SwaggerDoc("projects", new OpenApiInfo { Title = "Projects API", Version = "v1" });

    // 🔑 Add JWT Bearer Auth
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token with Bearer prefix (e.g. 'Bearer eyJhbGciOi...')",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // 🔒 Require security scheme for protected endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();
app.UseExceptionMiddleware();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/bugs/swagger.json", "Bugs API v1");
        c.SwaggerEndpoint("/swagger/projects/swagger.json", "Projects API v1");
        c.RoutePrefix = "swagger"; // optional: makes Swagger UI load at https://localhost:7128/
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
