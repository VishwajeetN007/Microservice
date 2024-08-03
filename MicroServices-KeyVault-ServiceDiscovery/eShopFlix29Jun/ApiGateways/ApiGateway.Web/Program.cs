using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("ocelot.json");
builder.Configuration.AddJsonFile("consul.json");
builder.Services
    .AddOcelot()
    .AddConsul();

var catalogAuthKey = builder.Configuration["Keys:CatalogService"];
builder.Services.AddAuthentication()
    .AddJwtBearer(catalogAuthKey, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();
app.UseOcelot().Wait();

app.Run();
