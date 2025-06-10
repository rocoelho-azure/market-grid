
using RabbIT.MarketGrid.Core.Services;
using RabbIT.MarketGrid.WebSocketServer.Servers;
using RabbIT.MarketGrid.WebSocketServer.Workers;

namespace RabbIT.MarketGrid.WebSocketServer
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            args = [];

            var builder = WebApplication.CreateBuilder();

            string? url = builder.Configuration["Host:BaseUrl"];
            if (url == null)
                throw new ApplicationException("KeyValue Host:BaseUrl not found in appsettings.json config file");

            builder.WebHost.UseUrls(url);


            builder.Services.AddSingleton<Servers.WebSocketServer, StockPriceWebSocketServer>();
            builder.Services.AddSingleton<IStockPriceService, StockPriceService>();
            builder.Services.AddHostedService<StockPriceUpdateWorker>();
            builder.Services.AddHostedService<BroadcastMessageWorker>();

            var app = builder.Build();

            app.UseWebSockets();

            app.Map("/ws", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var webSocketServer = context.RequestServices.GetRequiredService<Servers.WebSocketServer>();
                    await webSocketServer.HandleClient(context, webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });

            app.Run();

        }

    }
}
