using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace Slants.WebApp.Client.Configuration
{
    public static class GrpcRegistration
    {
        public static void UseGrpcClient(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSingleton(services =>
            {
                var navigationManager = services.GetRequiredService<NavigationManager>();
                var backendUrl = navigationManager.BaseUri;
                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

                return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
            });
        }

        public static void AddGrpcServiceClient<T>(this WebAssemblyHostBuilder builder) where T : class
        {
            builder.Services.AddTransient<T>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<T>();
            });
        }
    }
}
