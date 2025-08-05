Instalar as dpendências do projeto:
```bash
dotnet restore
```

Executar o Stryker. Você pode executar o Stryker diretamente no diretório do projeto ou especificar o caminho do projeto:
```bash
dotnet stryker
```

Executar o Microbenchmark. Entre no diretório do microbenchmark e execute: 
```bash
dotnet run -c release
```