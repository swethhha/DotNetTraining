using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopTrackPro.API.Extensions;
using ShopTrackPro.API.Middleware;
using ShopTrackPro.Application.Mapping;
using ShopTrackPro.Application.Services;
using ShopTrackPro.Core.Interfaces;
using ShopTrackPro.Infrastructure.Data;
using ShopTrackPro.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===== Add Services =====
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

// ===== DbContext =====
builder.Services.AddDbContext<ShopTrackProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ===== Authentication & JWT =====
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
                Encoding.UTF8.GetBytes(jwtConfig["Key"] ?? throw new ArgumentNullException("Jwt:Key")))
        };
    });

// ===== Authorization Policies =====
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireSeller", policy => policy.RequireRole("Seller"));
    options.AddPolicy("RequireUser", policy => policy.RequireRole("User"));
});

// ===== Controllers =====
builder.Services.AddControllers();

// ===== Swagger with JWT Support & Multiple Groups =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Create separate Swagger docs for each controller group
    c.SwaggerDoc("auth", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    c.SwaggerDoc("users", new OpenApiInfo { Title = "Users API", Version = "v1" });
    c.SwaggerDoc("products", new OpenApiInfo { Title = "Products API", Version = "v1" });
    c.SwaggerDoc("orders", new OpenApiInfo { Title = "Orders API", Version = "v1" });
    c.SwaggerDoc("orderitems", new OpenApiInfo { Title = "OrderItems API", Version = "v1" });

    // JWT Bearer security
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT token with Bearer prefix (e.g. 'Bearer eyJhbGciOi...')",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });

    // Map controllers to their Swagger group
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
        var groupName = apiDesc.ActionDescriptor.EndpointMetadata
            .OfType<Microsoft.AspNetCore.Mvc.ApiExplorerSettingsAttribute>()
            .FirstOrDefault()?.GroupName;
        return groupName == docName;
    });
});

var app = builder.Build();

// ===== Middleware =====
app.UseExceptionMiddleware();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/auth/swagger.json", "Auth API v1");      
        c.SwaggerEndpoint("/swagger/users/swagger.json", "Users API v1");
        c.SwaggerEndpoint("/swagger/products/swagger.json", "Products API v1");
        c.SwaggerEndpoint("/swagger/orders/swagger.json", "Orders API v1");
        c.SwaggerEndpoint("/swagger/orderitems/swagger.json", "OrderItems API v1");
        c.RoutePrefix = "swagger"; // optional
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
