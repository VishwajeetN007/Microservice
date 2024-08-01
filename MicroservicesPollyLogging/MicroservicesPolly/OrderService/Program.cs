using Polly;
using Polly.Extensions.Http;
using Polly.Retry;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//// This is mainly used for synchronous call and not for asynchronous.
builder.Services.AddHttpClient("Inventory")
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(RetryPolicyHandler())
    .AddPolicyHandler(CircuitBreakerPolicyHandler());


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


static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicyHandler()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        // After 20 Seconds, it will open the circuit and not allowing call.
        // During this time if we call try to call Inventory Service,it will not call.
        // After 20 Seconds,the service is avaialable to make for call.
        .CircuitBreakerAsync(3, TimeSpan.FromSeconds(20)); 
}
static IAsyncPolicy<HttpResponseMessage> RetryPolicyHandler()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(10)); // After every 10 Seconds, it will retry for the service call.
}