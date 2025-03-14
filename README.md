# Projeto de Visualiza��o de Pre�os de A��es em Tempo Real

Este reposit�rio cont�m um projeto desenvolvido em .NET 8 dividido em dois m�dulos principais:
1. **WebSocket Server**: Respons�vel por simular e emitir altera��es nos pre�os das a��es do mercado em tempo real.
2. **WPF Client**: Aplicativo de interface gr�fica desenvolvido em WPF para visualizar em tempo real as altera��es dos pre�os das a��es.

## Tecnologias e Bibliotecas Utilizadas
- **.NET 8** - Framework principal do projeto
- **WPF.UI** - Biblioteca para estiliza��o e componentes visuais no WPF
- **CommunityToolkit.Mvvm** - Implementa��o do padr�o MVVM para o WPF
- **Rx.NET** - Biblioteca para manipula��o reativa de eventos

## Estrutura do Projeto
```
/RabbIT.MarketGrid
  /RabbIT.MarketGrid.UI						# Aplicativo WPF para visualiza��o dos pre�os
  /RabbIT.MarketGrid.WebSocketServer		# Servidor WebSocket para emiss�o de pre�os
  /RabbIT.MarketGrid.Core					# Biblioteca de classes compartilhadas
```

## Como Executar
### Passos para Rodar o WebSocket Server
Ap�s clonar o reposir�rio, siga os passos abaixo para executar o projeto:

1. Navegue at� a pasta `WebSocketServer`.
   ```cmd
   cd /RabbIT.MarketGrid/RabbIT.MarketGrid.WebSocketServer   
   ```
1. Execute o comando:
   ```cmd   
   dotnet run
   ```
3. O servidor iniciar� e ficar� pronto para emitir eventos de pre�o.

### Passos para Rodar o WPF Client
1. Navegue at� a pasta `RabbIT.MarketGrid.UI`.
   ```cmd
   cd /RabbIT.MarketGrid/RabbIT.MarketGrid.UI
   ```
2. Execute o comando:
   ```cmd
   dotnet run
   ```
3. O aplicativo WPF iniciar� para exibir os pre�os das a��es em tempo real.
4. Navegue at� Stock Prices
5. Para visualizar as altera��es de pre�o, clique no bot�o "Start" no canto superior esquerdo.
6. Selecione uma a��o na lista de a��es dispon�veis e clique em adicionar.
7. A a��o ser� adicionada ao grid de pre�os e as altera��es de pre�o ser�o exibidas em tempo real.



