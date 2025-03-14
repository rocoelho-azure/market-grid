using RabbIT.MarketGrid.Core.Services;

namespace RabbIT.MarketGrid.WebSocketServer.Workers
{
    public class StockPriceUpdateWorker : BackgroundService
    {

        private readonly IStockPriceService _service;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(1);

        public StockPriceUpdateWorker(IStockPriceService service)
        {            
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _service.UpdateStockPrices();
                await Task.Delay(_updateInterval, stoppingToken);
            }
        }
    }
}
