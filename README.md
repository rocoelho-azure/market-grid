# Projeto de Visualização de Preços de Ações em Tempo Real

Este repositório contém um projeto desenvolvido em .NET 8 dividido em dois módulos principais:
1. **App Host**: Aplicativo responsável por gerenciar e monitorar os serviços da solução
2. **WebSocket Server**: Responsável por simular e emitir alterações nos preços das ações do mercado em tempo real.
3. **WPF Client**: Aplicativo de interface gráfica desenvolvido em WPF para visualizar em tempo real as alterações dos preços das ações.

## Tecnologias e Bibliotecas Utilizadas
- **.NET 8** - Framework principal do projeto
- **WPF.UI** - Biblioteca para estilização e componentes visuais no WPF
- **CommunityToolkit.Mvvm** - Implementação do padrão MVVM para o WPF
- **Rx.NET** - Biblioteca para manipulação reativa de eventos
- **.net Aspire** - Ferramenta da Microsoft para orquestrar serviços e suas dependencias

## Estrutura do Projeto
```
/RabbIT.MarketGrid
  /RabbIT.MarketGrid.AppHost
    /RabbIT.MarketGrid.AppHost                  # Aplicativo que gerencia os demais aplicativos
    /RabbIT.MarketGrid.AppHost.ServiceDefaults  # Projeto que inicializa configurações padrões nos aplicativos e.g Monitoramento
  /RabbIT.MarketGrid.UI                         # Aplicativo WPF para visualização dos preços
  /RabbIT.MarketGrid.WebSocketServer            # Servidor WebSocket para emissão de preços
  /RabbIT.MarketGrid.Core                       # Biblioteca de classes compartilhadas
```

## Como Executar
### Passos para Rodar o WebSocket Server
Após clonar o repositório, siga os passos abaixo para executar o projeto:

1. Navegue até a pasta `RabbIT.MarketGrid.AppHost`.
   ```cmd
   cd /RabbIT.MarketGrid/RabbIT.MarketGrid.AppHost/RabbIT.MarketGrid.AppHost   
   ```
2. Execute o comando:
   ```cmd   
   dotnet run
   ```
3. O .net Aspire irá inicializar os serviços e disponibilizará o dashboard em https://localhost:17296/
4. O aplicativo WPF iniciará para exibir os preços das ações em tempo real.
5. Navegue até Stock Prices.
6. Para visualizar as alterações de preço, clique no botão "Start" no canto superior esquerdo.
7. Selecione uma ação na lista de ações disponíveis e clique em adicionar.
8. A ação será adicionada ao grid de preços e as alterações de preço serão exibidas em tempo real.



