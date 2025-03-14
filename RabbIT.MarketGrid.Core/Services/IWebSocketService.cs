
using RabbIT.MarketGrid.Core.Model;


namespace RabbIT.MarketGrid.Core.Services
{
    public interface IWebSocketService
    {
        IObservable<IEnumerable<Stock>> StockPrices
        {
            get;
        }

        IObservable<string> ConnectionStatus
        {
            get;
        }
        public Task StopReceivingMessages(CancellationToken cancellation);
        public Task StartReceivingMessages(string uri,CancellationToken cancellationToken);
        public Task SendMessage(string code, CancellationToken cancellationToken);
    }
}