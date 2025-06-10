var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RabbIT_MarketGrid_UI>("MarketGridUI");
builder.AddProject<Projects.RabbIT_MarketGrid_WebSocketServer>("MarketGridWebSocketServer"); 

builder.Build().Run();
