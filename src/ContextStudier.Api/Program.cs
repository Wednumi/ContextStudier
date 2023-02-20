using ContextStudier.Core;
using ContextStudier.Infrastructure;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ContextStudier.Core.Interfaces.Security;
using ContextStudier.Api.DIExtensions;

var allowedOrigins = "_allowedOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSetSwagger();
builder.Services.AddSetCors(allowedOrigins, builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSetSecurity();
builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowedOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
