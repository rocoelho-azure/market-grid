using CommunityToolkit.Mvvm.Input;

using Wpf.Ui.Appearance;

namespace RabbIT.MarketGrid.UI.ViewModel
{
    public partial class SettingsViewModel
    {
        [RelayCommand]
        void ApplyDarkTheme()
        {
            ApplicationThemeManager.Apply(ApplicationTheme.Dark);
            
        }

        [RelayCommand]
        void ApplyLightTheme()
        {
            ApplicationThemeManager.Apply(ApplicationTheme.Light);
            
        }

    }
}
