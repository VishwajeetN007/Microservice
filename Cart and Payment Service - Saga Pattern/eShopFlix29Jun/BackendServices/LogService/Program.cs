using Serilog.Sinks.Elasticsearch;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var uri = new Uri(builder.Configuration["ElasticSearch:Uri"]);
var sinkOptions = new ElasticsearchSinkOptions(uri)
{
    DetectElasticsearchVersion = false,
    AutoRegisterTemplate = true,
    ModifyConnectionSettings = x => x.BasicAuthentication(builder.Configuration["ElasticSearch:Username"], builder.Configuration["ElasticSearch:Password"]),
    IndexFormat = $"logs-{0:yyyy.MM}",
};
builder.Host.UseSerilog((ctx, lc) => lc
.Enrich.FromLogContext()
.Enrich.WithMachineName()
.WriteTo.Elasticsearch(sinkOptions).ReadFrom.Configuration(ctx.Configuration));

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
