using Microsoft.Extensions.DependencyInjection;
using RabbIT.MarketGrid.UI.Services;
using RabbIT.MarketGrid.UI.ViewModel;

using System.Windows;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace RabbIT.MarketGrid.UI
{
   
    public partial class MainWindow
    {
        public MainWindow(
           
            ISnackbarService snackbarService,
            INavigationService navigationService)
        {
            InitializeComponent();
            DataContext = this;

            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            navigationService.SetNavigationControl(RootNavigation);
        }
    }
}
