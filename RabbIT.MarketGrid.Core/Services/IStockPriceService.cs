using RabbIT.MarketGrid.Core.Model;

namespace RabbIT.MarketGrid.Core.Services
{
    public interface IStockPriceService
    {
        public void AddClientStock(Guid clientId, string? code);
        public void RemoveClient(Guid clientId);
        public IEnumerable<Stock> GetStockPricesPrices(Guid guid);
        public void UpdateStockPrices();
    }
}
