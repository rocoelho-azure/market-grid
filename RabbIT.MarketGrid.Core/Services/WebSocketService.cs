using Newtonsoft.Json;
using RabbIT.MarketGrid.Core.Model;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace RabbIT.MarketGrid.Core.Services
{
    public class WebSocketService : IWebSocketService
    {
        private ClientWebSocket _webSocket = new ClientWebSocket();
        private readonly Subject<IEnumerable<Stock>> _stockPricesSubject;
        private readonly Subject<string> _connectionStatusSubject;

        private Dictionary<string, Stock> _lastStockPrices;
        public IObservable<IEnumerable<Stock>> StockPrices => _stockPricesSubject.AsObservable();
        public IObservable<string> ConnectionStatus => _connectionStatusSubject.AsObservable();

        public WebSocketService()
        {

            _stockPricesSubject = new Subject<IEnumerable<Stock>>();
            _connectionStatusSubject = new Subject<string>();
            _lastStockPrices = new List<Stock>().ToDictionary(item => item.Code);
        }

        private Task Connect(string uri, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException(nameof(uri));

          
            _webSocket = new ClientWebSocket();
            return _webSocket.ConnectAsync(new Uri(uri), cancellationToken);

        }

        public async Task StartReceivingMessages(string uri, CancellationToken cancellationToken)
        {
            try
            {
                _connectionStatusSubject.OnNext("Trying to connect");

                await Connect(uri, cancellationToken);

                _connectionStatusSubject.OnNext("Connected. Receiving data");

                var buffer = new byte[1024];
                while (_webSocket.State == WebSocketState.Open)
                {
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var stockPrices = ComputeStatusStock(GetStockPrices(buffer, result));

                        _stockPricesSubject.OnNext(stockPrices);

                        _lastStockPrices = stockPrices.ToDictionary(item => item.Code);

                    }
                }
            }
            catch (Exception)
            {
                _connectionStatusSubject.OnNext("Error start connection");
                throw;
            }
        }

        private IEnumerable<Stock> GetStockPrices(byte[] buffer, WebSocketReceiveResult result)
        {
            var emptyDictionary = new List<Stock>();
            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var stockPrices = JsonConvert.DeserializeObject<IEnumerable<Stock>>(message);

            if (stockPrices == null)
                return emptyDictionary;

            return stockPrices;
        }

        private IEnumerable<Stock> ComputeStatusStock(IEnumerable<Stock> currentStockPrice)
        {
            var dictA = currentStockPrice.ToDictionary(item => item.Code);

            foreach (var item in _lastStockPrices)
            {
                var lastPrice = item.Value.Price;
                var currentPrice = dictA[item.Key].Price;
                dictA[item.Key].Status = StatusStock.Steel;

                if (currentPrice > lastPrice)
                    dictA[item.Key].Status = StatusStock.Up;

                if (currentPrice < lastPrice)
                    dictA[item.Key].Status = StatusStock.Down;

                dictA[item.Key].Price = currentPrice;
            }

            return dictA.Values.ToList();
        }

        public async Task StopReceivingMessages(CancellationToken cancellationToken)
        {
            switch (_webSocket.State)
            {
                case WebSocketState.Open:
                case WebSocketState.CloseReceived:
                case WebSocketState.CloseSent:
                    {
                        _connectionStatusSubject.OnNext("Trying to stop");

                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal Closure", cancellationToken);
                        _webSocket.Abort();

                        _lastStockPrices.Clear();

                        _connectionStatusSubject.OnNext("Stopped");

                        return;
                    }
                default:
                    break;

            }
        }

        public async Task SendMessage(string code, CancellationToken cancellationToken)
        {
            if (_webSocket.State == WebSocketState.Open)
                await _webSocket.SendAsync(Encoding.ASCII.GetBytes(code), WebSocketMessageType.Text, true, cancellationToken);
        }
    }
}
