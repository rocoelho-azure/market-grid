using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using RabbIT.MarketGrid.Core.Model;
using RabbIT.MarketGrid.Core.Services;
using RabbIT.MarketGrid.UI.Decorators;
using Wpf.Ui;
using Wpf.Ui.Controls;


namespace RabbIT.MarketGrid.UI.ViewModel
{
    public partial class StockPriceViewModel : ObservableObject
    {
        private readonly IConfiguration _config;
        private readonly IWebSocketService _webSocketService;
        private readonly ISnackbarService _snackbarService;
        public StockPriceViewModel(
            IWebSocketService webSocketService, 
            ISnackbarService snackbarService,
            IConfiguration config)
        {
            _webSocketService = webSocketService;
            _snackbarService = snackbarService;

            _config = config;

            _webSocketService.StockPrices.Subscribe(UpdateStocks);
            _webSocketService.ConnectionStatus.Subscribe(UpdateConnectionStatus);

            Stocks = new List<StockDecorator>();
            Code = string.Empty;
            ConnectionStatus = string.Empty;
        }

        [ObservableProperty]
        string code;

        [ObservableProperty]
        List<StockDecorator> stocks;

        [ObservableProperty]
        string connectionStatus;

        [ObservableProperty]
        bool canSendCommand;

        partial void OnCodeChanged(string? oldValue, string newValue)
        {
            CanSendCommand = !string.IsNullOrEmpty(newValue);
        }

        [RelayCommand(IncludeCancelCommand=true)]
        async Task Start(CancellationToken cancellationToken)
        {
            try
            {
                string? url = TryGetWebSocketBaseUrl();
                await _webSocketService.StartReceivingMessages(url, cancellationToken);

            }
            catch (Exception ex)
            {
                _snackbarService.Show(
                    "Error", 
                    "Error starting connection: " + ex.Message, ControlAppearance.Danger,
                    new SymbolIcon(SymbolRegular.ErrorCircle20), TimeSpan.FromSeconds(1000));

            }
        }

        private string TryGetWebSocketBaseUrl()
        {
            var configKey = "WebSocketSettings:BaseUrl";
            string? url = _config[configKey];
            if (string.IsNullOrEmpty(url))
                throw new ApplicationException($"KeyValue {configKey} not found in appsettings.json config file");
            return url;
        }

        [RelayCommand(IncludeCancelCommand=true)]
        Task Send(CancellationToken cancellationToken)
        {
            return _webSocketService.SendMessage(Code, cancellationToken);
        }

        [RelayCommand(IncludeCancelCommand=true)]
        Task Stop(CancellationToken cancellationToken)
        {
            return _webSocketService.StopReceivingMessages(cancellationToken);
        }

        private void UpdateStocks(IEnumerable<Stock> data)
        {
            Stocks = [.. data.Select(stock => new StockDecorator(stock))];            
        }

        private void UpdateConnectionStatus(string status)
        {   
            ConnectionStatus = status;
        }
    }
}

