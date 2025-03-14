
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace RabbIT.MarketGrid.WebSocketServer.Servers
{
    public abstract class GenericWebSocketServer
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _clients = new();
        private readonly object _lock = new();
        public abstract void OnOpen(Guid clientId);
        public abstract void OnReceiveMessage(Guid clientId, string message);
        public abstract object GetMessageToSend(Guid clientId);
        public abstract void OnClose(Guid clientId);
        
        public async Task HandleClient(HttpContext context, WebSocket webSocket)
        {
            Guid clientId = Guid.NewGuid();
            lock (_lock)
            {
                _clients[clientId] = webSocket;
            }

            OnOpen(clientId);

            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        lock (_lock)
                        {
                            _clients.Remove(clientId, out _);
                        }
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                        OnClose(clientId);
                    }
                    else if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        OnReceiveMessage(clientId, message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Client disconnected unexpected");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task BroadcastMessageAsync()
        {
            var tasks = _clients.Keys.Select(client =>
            {
                var jsonMessage = JsonSerializer.Serialize(GetMessageToSend(client));
                var messageBuffer = Encoding.UTF8.GetBytes(jsonMessage);

                if (_clients[client].State == WebSocketState.Open)
                {
                    return _clients[client].SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, false, CancellationToken.None);
                
                }
                return Task.CompletedTask;

            });

            await Task.WhenAll(tasks);
        }
    }
}
