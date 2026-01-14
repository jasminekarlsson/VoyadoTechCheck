using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechCheck.Domain;
using TechCheck.Web.Proxy;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7222") });
builder.Services.AddScoped<ISearchWebService, WebHttpProxy>();
await builder.Build().RunAsync();
