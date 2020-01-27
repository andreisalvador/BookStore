# andrei-teste

- IDE utilizada para desenvolvimento: VSCode
- Tecnologia: .NET Core 3.0
- Banco de Dados: SQLite
- Estrutura da Aplicaçõa: DDD (Domain-Drive Design).
- Framework ORM: EntityFramework Core.

Para deixar a WebApi ativa, basta acessar a pasta clonada (andrei-teste) e executar o comando "dotnet restore" (baixar a SDK caso não possua no link: https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install).

Após realizado o restore das dependências, basta execute o comando "dotnet build" para buildar a aplicação.

Feito isso, navegue até o diretorio BookStore.Application e execute o comando "dotnet run" e a WebApi estara executando.

Com a WebApi executando, abra o arquivo "index.html" e insira, exclua e edite livros.