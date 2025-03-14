# Projeto de Visualização de Preços de Ações em Tempo Real

Este repositório contém um projeto desenvolvido em .NET 8 dividido em dois módulos principais:
1. **WebSocket Server**: Responsável por simular e emitir alterações nos preços das ações do mercado em tempo real.
2. **WPF Client**: Aplicativo de interface gráfica desenvolvido em WPF para visualizar em tempo real as alterações dos preços das ações.

## Tecnologias e Bibliotecas Utilizadas
- **.NET 8** - Framework principal do projeto
- **WPF.UI** - Biblioteca para estilização e componentes visuais no WPF
- **CommunityToolkit.Mvvm** - Implementação do padrão MVVM para o WPF
- **Rx.NET** - Biblioteca para manipulação reativa de eventos

## Estrutura do Projeto
```
/RabbIT.MarketGrid
  /RabbIT.MarketGrid.UI						# Aplicativo WPF para visualização dos preços
  /RabbIT.MarketGrid.WebSocketServer		# Servidor WebSocket para emissão de preços
  /RabbIT.MarketGrid.Core					# Biblioteca de classes compartilhadas
```

## Como Executar
### Passos para Rodar o WebSocket Server
Após clonar o reposirório, siga os passos abaixo para executar o projeto:

1. Navegue até a pasta `WebSocketServer`.
   ```cmd
   cd /RabbIT.MarketGrid/RabbIT.MarketGrid.WebSocketServer   
   ```
1. Execute o comando:
   ```cmd   
   dotnet run
   ```
3. O servidor iniciará e ficará pronto para emitir eventos de preço.

### Passos para Rodar o WPF Client
1. Navegue até a pasta `RabbIT.MarketGrid.UI`.
   ```cmd
   cd /RabbIT.MarketGrid/RabbIT.MarketGrid.UI
   ```
2. Execute o comando:
   ```cmd
   dotnet run
   ```
3. O aplicativo WPF iniciará para exibir os preços das ações em tempo real.
4. Navegue até Stock Prices
5. Para visualizar as alterações de preço, clique no botão "Start" no canto superior esquerdo.
6. Selecione uma ação na lista de ações disponíveis e clique em adicionar.
7. A ação será adicionada ao grid de preços e as alterações de preço serão exibidas em tempo real.



