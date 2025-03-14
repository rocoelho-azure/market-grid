using RabbIT.MarketGrid.UI.ViewModel;


namespace RabbIT.MarketGrid.UI.Pages;

public partial class SettingsPage
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

    }
}
