using InventoryService.Consumers;
using MassTransit;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderConsumer>();

    config.UsingRabbitMq((context, cfg) => {

        var uri = new Uri(builder.Configuration["ServiceBus:Uri"]);

        cfg.Host( uri, host => {
            host.Username(builder.Configuration["ServiceBus:Username"]);
            host.Password(builder.Configuration["ServiceBus:Password"]);
        });

        var queueName = builder.Configuration["ServiceBus:Queue"];
        cfg.ReceiveEndpoint(queueName, e => {

            /* Code to configure exchange type
              
            e.Bind(builder.Configuration["ServiceBus:Exchange"], x =>
            {
                //x.ExchangeType =  ExchangeType.Direct; // We can setup ExchangeTyp (Enum) as Fanout,Topic,Header
                // or
                x.ExchangeType = "direct"; // We can setup ExchangeTyp as Fanout,Topic,Header
                x.RoutingKey = builder.Configuration["ServiceBus:RoutingKey"];
            });
           */
            e.ConfigureConsumer<OrderConsumer>(context);
        });
    });
});

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
