using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    using Domain.Lancamentos.Entities;
    
    public class ConsolidarLancamentosCommandHandler : IAsyncCommandHandler<ConsolidarLancamentosCommand>
    {
        private readonly ICommandBus _commandBus;
        private readonly ILancamentos _lancamentos;        
        public ConsolidarLancamentosCommandHandler(ILancamentos lancamentos, ICommandBus commandBus)
        {
            _lancamentos = lancamentos;
            _commandBus = commandBus;
        }

        private async Task EnviarParaProcessamento(Lancamento lancamento)
        {
            switch (lancamento.Tipo)
            {
                case TipoLancamento.Pagamento:
                    await _commandBus.SendAsync(new ProcessarPagamentoCommand(lancamento));
                    break;
                case TipoLancamento.Recebimento:
                    await _commandBus.SendAsync(new ProcessarRecebimentoCommand(lancamento));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public async Task Handle(CommandContext<ConsolidarLancamentosCommand> context)
        {
            var data = DateTime.Parse(context.Command.Input.Data, new CultureInfo("pt-BR"));
            var lancamentosPorData = _lancamentos.GetByData(data).ToList();
            foreach (var lancamento in lancamentosPorData)
            {
               await this.EnviarParaProcessamento(lancamento);
            }            
        }
    }
}