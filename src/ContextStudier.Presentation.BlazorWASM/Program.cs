using ContextStudier.Presentation.BlazorWASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = 
    new Uri(builder.Configuration.GetValue<string>("ContextStudierApi"))});

await builder.Build().RunAsync();