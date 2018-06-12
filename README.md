# fluxo-caixa

A solução consiste em:

* web api - API Rest com as rotas para execução do fluxo de lançamentos, consolidações e consultas. Responsável pelo handler do comando `ReceberLancamentosCommand`
* worker - App Console responsável pelos handlers dos comandos `ProcessarPagamentoCommand`, `ProcessarRecebimentoCommand` e `ConsolidarLancamentos`
* mensageria com RabbitMQ - infraestrutura de message broker
* database com SQL Server - persistência dos dados


## Instruções para execução com Docker

```
docker-compose up
```

## Web API

A web api deverá estar disponível em `http://localhost:5001/swagger` após a inicialização bem-sucedida com a interface do Swagger UI padrão disponível para explorar a API.




