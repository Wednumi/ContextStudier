using ContextStudier.Infrastructure;
using System.Reflection;
using ContextStudier.Api.DIExtensions;
using ContextStudier.Core;

var allowedOrigins = "_allowedOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCoreServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSetSwagger();
builder.Services.AddSetCors(allowedOrigins, builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), 
    Assembly.Load("ContextStudier.Core"));
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
