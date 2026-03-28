using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StayFocus;
using StayFocus.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register IConfiguration so services can read values from wwwroot/appsettings.json
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configure HttpClient base address from configuration when available so the
// deployed app calls the correct API host. Falls back to the app host origin.
var apiBase = builder.Configuration["ApiBaseUrl"];
if (!string.IsNullOrWhiteSpace(apiBase))
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });
}
else
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
}

// Register the Review API client
builder.Services.AddScoped<ReviewApiClient>();
// Do not register a hard-coded configuration here. Blazor WebAssembly's
// CreateDefault already loads wwwroot/appsettings.json and environment
// configuration for ApiBaseUrl. If you need to set a production API URL,
// add wwwroot/appsettings.json with { "ApiBaseUrl": "https://api.example.com" }
// or set the ApiBaseUrl environment variable at build/publish time.

await builder.Build().RunAsync();
