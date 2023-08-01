using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Slants.Services;
using Slants.WebApp.Client;
using Slants.WebApp.Client.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// gRPC services
builder.UseGrpcClient();
builder.AddGrpcServiceClient<ISlantsService>();
builder.AddGrpcServiceClient<ITopicsService>();

await builder.Build().RunAsync();
