﻿using RabbIT.MarketGrid.WebSocketServer.Servers;

namespace RabbIT.MarketGrid.WebSocketServer.Workers
{
    public class BroadcastMessageWorker : BackgroundService
    {
        
        private readonly Servers.WebSocketServer _webSocketServer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(1);

        public BroadcastMessageWorker(Servers.WebSocketServer webSocketServer)
        {            
            _webSocketServer = webSocketServer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _webSocketServer.BroadcastMessageAsync();
                await Task.Delay(_updateInterval, stoppingToken);
            }
        }
    }
}
