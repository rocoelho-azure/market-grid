using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbIT.MarketGrid.Core.Services;
using RabbIT.MarketGrid.UI.Pages;
using RabbIT.MarketGrid.UI.Services;
using RabbIT.MarketGrid.UI.ViewModel;

using System.Windows;
using System.Windows.Threading;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;

namespace RabbIT.MarketGrid.UI;

public partial class App
{
    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => c.SetBasePath(AppContext.BaseDirectory))
        .ConfigureServices(ConfigureApplicationServices)
        .Build();

    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddNavigationViewPageProvider();

        services.AddHostedService<ApplicationHostService>();

        services.AddSingleton<MainWindow>();
        
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<IContentDialogService, ContentDialogService>();
        services.AddSingleton<IWebSocketService, WebSocketService>();

        services.AddSingleton<StockPriceViewModel>();
        services.AddSingleton<StockPriceViewPage>();

        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<SettingsPage>();

        services.AddSingleton<AboutPage>();
    }

    public static T GetRequiredService<T>()
        where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }
 
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
    }
   
    private void OnExit(object sender, ExitEventArgs e)
    {
        _host.StopAsync().Wait();
        _host.Dispose();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
    }

}

