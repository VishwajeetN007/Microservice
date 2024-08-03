using Azure.Identity;
using KeyVault.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add keyvault services to the container.
builder.Services.AddAzureClients(azureClientFactoryBuilder =>
{
    var valutUri = new Uri(builder.Configuration["KeyVault:VaultUri"]);

    azureClientFactoryBuilder.AddSecretClient(valutUri).WithCredential(new DefaultAzureCredential());
});
builder.Services.AddScoped<IKeyVaultService, KeyVaultService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
