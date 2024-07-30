using CommonLibrary.Database;
using CommonLibrary.Messages.Commands;
using CommonLibrary.StateMachine;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Consumers;
using OrderService.Database;
using OrderService.HttpClients;
using OrderService.ServiceBus;
using OrderService.Services.Implementations;
using OrderService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddHttpClient<CartService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiAddress:Cart"]);
});


builder.Services.AddScoped<IOrderDataService, OrderDataService>();

//// Add Services to the container for Azure Bus.
//builder.Services.AddSingleton<IOrderConsumer, OrderConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.AddRequestClient<IOrderInitiate>();

    config.AddConsumer<OrderStartConsumer>();
    config.AddConsumer<OrderCancelledConsumer>();
    config.AddConsumer<OrderAcceptedConsumer>();

    // State machine
    config.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
            r.AddDbContext<DbContext, OrderStateContext>((provider, options) =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            });
        });

    // Configure the rabbitmq
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.ConfigureEndpoints(ctx);
        var uri = new Uri("rabbitmq://localhost/");
        cfg.Host(uri, host =>
        {
            host.Username("guest");
            host.Password("guest");
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

////Register Receiver message of Azure Service Bus.
//var bus = app.Services.GetService<IOrderConsumer>();
//bus?.RegisterReceiveMessageHandler();

app.Run();
