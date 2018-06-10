using System;
using System.Threading.Tasks;
using Stone.Lancamento.Application.Events;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    public class ProcessarRecebimentoCommandHandler : IAsyncCommandHandler<ProcessarRecebimentoCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ProcessarRecebimento _processarRecebimento;
        public ProcessarRecebimentoCommandHandler(ProcessarRecebimento processarRecebimento, IEventBus eventBus)
        {
            _processarRecebimento = processarRecebimento;
            _eventBus = eventBus;
        }

        public async Task Handle(CommandContext<ProcessarRecebimentoCommand> context)
        {
            var lancamento = context.Command.Input;
            try
            {                
                await _processarRecebimento.Apply(lancamento);
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id));
            }
            catch (Exception e)
            {
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id, e));
            }
        }
    }
}