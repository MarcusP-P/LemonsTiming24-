using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LemonsTiming24.Client;

#pragma warning disable CA1852 // Seal internal types
var builder = WebAssemblyHostBuilder.CreateDefault(args);
#pragma warning restore CA1852 // Seal internal types
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
