using Microsoft.AspNetCore.Components;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace Slants.WebApp.Client.Configuration
{
    public static class GrpcRegistration
    {
        public static void UseGrpcClient(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton(services =>
            {
                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

                // TODO: Replace with your own remote address with config
                return GrpcChannel.ForAddress("https://localhost:7138", new GrpcChannelOptions { HttpHandler = httpHandler });
            });
        }

        public static void AddGrpcServiceClient<T>(this MauiAppBuilder builder) where T : class
        {
            builder.Services.AddTransient<T>(services =>
            {
                var grpcChannel = services.GetRequiredService<GrpcChannel>();
                return grpcChannel.CreateGrpcService<T>();
            });
        }
    }
}
