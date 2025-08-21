using HostelManagement.Application.Services;
using HostelManagement.Core.Interfaces;
using HostelManagement.Infrastructure.Repositories;
using HostelManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Register In-Memory Repositories
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IStaffRepository, StaffRepository>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

// 🔹 Register Services
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStudentService, StudentService>();

// 🔹 Add Controllers & Swagger
builder.Services.AddControllers();
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
