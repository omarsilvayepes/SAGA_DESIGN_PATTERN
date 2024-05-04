using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order_ms.Data;
using Order_ms.Services;
using RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrderDbContext>(options =>
     options.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderDataAccess, OrderDataAccess>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MassTransive service publisher

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider));
});

builder.Services.AddMassTransitHostedService();

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
