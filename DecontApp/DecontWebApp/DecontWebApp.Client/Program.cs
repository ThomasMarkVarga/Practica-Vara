using DecontWebApp.Client.Pages;
using DecontWebApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ITipCheltuialaService, TipCheltuialaService>();

await builder.Build().RunAsync();
