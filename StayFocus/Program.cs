using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StayFocus;
using StayFocus.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register the Review API client
builder.Services.AddScoped<ReviewApiClient>();
builder.Services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
    .AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(@"
    {
      ""ApiBaseUrl"": ""http://localhost:5000""
    }")))
    .Build());

await builder.Build().RunAsync();
