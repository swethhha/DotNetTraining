using HostelManagement.API.Extensions;
using HostelManagement.Application.Services;
using HostelManagement.Core.Interfaces;
using HostelManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Register In-Memory Repositories
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IStaffRepository, StaffRepository>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

// 🔹 Register Services
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStudentService, StudentService>();

// 🔹 Configure JWT Authentication (Optional)
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
    options.AddPolicy("RequireStaff", policy => policy.RequireRole("Staff"));
});


// 🔹 Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("hostelmanagement", new OpenApiInfo
    {
        Title = "Hostel Management API",
        Version = "v1",
        Description = "Combined API documentation for Rooms, Staffs, and Students"
    });
    c.SwaggerDoc("rooms", new OpenApiInfo { Title = "Rooms API", Version = "v1" });
    c.SwaggerDoc("staffs", new OpenApiInfo { Title = "Staffs API", Version = "v1" });
    c.SwaggerDoc("students", new OpenApiInfo { Title = "Students API", Version = "v1" });

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

// 🔹 Custom Exception Middleware (Optional)
app.UseExceptionMiddleware();

// 🔹 Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/hostelmanagement/swagger.json", "Hostel Management API v1");
        c.SwaggerEndpoint("/swagger/rooms/swagger.json", "Rooms API v1");
        c.SwaggerEndpoint("/swagger/staffs/swagger.json", "Staffs API v1");
        c.SwaggerEndpoint("/swagger/students/swagger.json", "Students API v1");
        c.RoutePrefix = "swagger"; 
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
