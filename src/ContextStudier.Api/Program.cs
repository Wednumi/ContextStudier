using ContextStudier.Core;
using ContextStudier.Infrastructure;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ContextStudier.Core.Interfaces.Security;

var allowedOrigins = "_allowedOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(o =>
{
    o.AddPolicy(allowedOrigins,
        policy =>
        {
            policy.WithOrigins(builder.Configuration["AllowedConsumer"])
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

using(var serviceProvider = builder.Services.BuildServiceProvider())
{
    var tokenSource = serviceProvider
        .GetRequiredService<ISecurityKeySource>();

    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "JwtBearer";
        option.DefaultChallengeScheme = "JwtBearer";
    })
        .AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenSource.GetKeyBytes().Result),
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5),
            };
        });
}

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "ContextStudier Api",
            Version = "v1",
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowedOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
