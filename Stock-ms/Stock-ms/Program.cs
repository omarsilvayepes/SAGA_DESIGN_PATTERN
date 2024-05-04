using MassTransit;
using RabbitMq;
using Stock_ms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add MssiveTransit Consumer service 


builder.Services.AddMassTransit(cfg =>
{
    //cfg.AddConsumer<OrderCardNumberValidateConsumer>();
    cfg.AddConsumer<OrderValidateConsumer>();

    cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider, (cfg, host) =>
    {
        //cfg.ReceiveEndpoint(BusConstants.OrderQueue, ep =>
        //{
        //    ep.ConfigureConsumer<OrderCardNumberValidateConsumer>(provider);
        //});

        cfg.ReceiveEndpoint(BusConstants.SagaBusQueue, ep =>
        {
            ep.ConfigureConsumer<OrderValidateConsumer>(provider);
        });

    }));
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
