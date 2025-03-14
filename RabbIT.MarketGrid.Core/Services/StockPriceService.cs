using RabbIT.MarketGrid.Core.Model;
using System.Collections.Concurrent;


namespace RabbIT.MarketGrid.Core.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly List<Stock> _stockPrices = new();
        private readonly ConcurrentDictionary<Guid, List<string>> _clientStocks = new();
        private readonly Random _random = new();

        public StockPriceService()
        {
            _stockPrices.Add(new Stock("AAPL", 500, StatusStock.Up) );
            _stockPrices.Add(new Stock("TSLA", 2800, StatusStock.Up));
            _stockPrices.Add(new Stock("MSFT", 330, StatusStock.Up));
            _stockPrices.Add(new Stock("AMZN", 3500, StatusStock.Up));
           
        }

        public void AddClientStock(Guid clientId, string? code)
        {
            if (!_clientStocks.ContainsKey(clientId))
                _clientStocks[clientId] = new List<string>();

            if (code != null && !_clientStocks[clientId].Contains(code))
                _clientStocks[clientId].Add(code);
        }

        public void RemoveClient(Guid clientId)
        {
            _clientStocks.Remove(clientId, out _);
        }

        private IEnumerable<Stock> GetCurrentPrices(Guid clientId)
        {
            return _stockPrices.Where(item => _clientStocks[clientId].Contains(item.Code)).ToList();
        }

        public IEnumerable<Stock> GetStockPricesPrices(Guid guid)
        {
            return GetCurrentPrices(guid);
        }
        public void UpdateStockPrices()
        {
            foreach (var item in _stockPrices.ToList())
            {
                var change = (float)(_random.NextDouble() * 5 - 2.5);
                item.Price = Math.Max(1, item.Price + change);
            }
        }
    }
}
