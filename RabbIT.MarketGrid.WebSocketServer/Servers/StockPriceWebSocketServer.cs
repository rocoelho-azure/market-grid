using RabbIT.MarketGrid.Core.Services;

namespace RabbIT.MarketGrid.WebSocketServer.Servers
{
    public class StockPriceWebSocketServer : WebSocketServer
    {
        private readonly IStockPriceService _stockPriceService;
        public StockPriceWebSocketServer(IStockPriceService stockPriceService)
        {
            _stockPriceService = stockPriceService;
        }
        public override void OnClose(Guid clientId)
        {
            _stockPriceService.RemoveClient(clientId);
        }

        public override void OnReceiveMessage(Guid clientId, string message)
        {
            _stockPriceService.AddClientStock(clientId, message);
        }

        public override void OnOpen(Guid clientId)
        {
            _stockPriceService.AddClientStock(clientId, null);
        }

        public override object GetMessageToSend(Guid clientId)
        {
            return _stockPriceService.GetStockPricesPrices(clientId);
        }
    }
}
