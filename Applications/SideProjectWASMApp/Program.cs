using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SideProjectWASMApp;
using NOAAWeather;

using ChessLibrary.Services.Engines;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<NOAAWeatherService>();
builder.Services.AddSingleton(s => new StockfishAPIClient(new HttpClient()));

await builder.Build().RunAsync();
