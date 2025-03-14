

using Microsoft.Extensions.DependencyInjection;
using RabbIT.MarketGrid.UI.ViewModel;

namespace RabbIT.MarketGrid.UI.Pages
{
    
    public partial class StockPriceViewPage 
    {
        public StockPriceViewPage(StockPriceViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        
    }
}
