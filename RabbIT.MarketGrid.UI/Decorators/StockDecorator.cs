using RabbIT.MarketGrid.Core;
using RabbIT.MarketGrid.Core.Model;

using System.Windows.Media;

namespace RabbIT.MarketGrid.UI.Decorators
{
    public class StockDecorator : Stock
    {

        public StockDecorator(Stock stock): base(stock.Code, stock.Price, stock.Status)  
        {
        }

        public Brush Indicator
        {
            get            
            {
                return Status == Core.StatusStock.Up ? Brushes.Green : Status == Core.StatusStock.Down ? Brushes.Red : Brushes.Transparent;
            }
        }
    }
}
