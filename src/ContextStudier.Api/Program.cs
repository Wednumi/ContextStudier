using ContextStudier.Core;
using ContextStudier.Infrastructure;
using System.Reflection;

var allowedOrigins = "_allowedOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy(allowedOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7111")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
