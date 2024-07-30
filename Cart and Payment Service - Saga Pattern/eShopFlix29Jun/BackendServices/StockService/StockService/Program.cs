using MassTransit;
using Microsoft.EntityFrameworkCore;
using StockService.Consumer;
using StockService.Database;
using StockService.Services.Implementations;
using StockService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    // Add consumers
    config.AddConsumer<StockValidateConsumer>();

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

builder.Services.AddScoped<IStockDataService, StockDataService>();

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
