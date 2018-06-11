using System;
using System.Threading.Tasks;
using Stone.Lancamento.Application.Events;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    public class ProcessarRecebimentoCommandHandler : IAsyncCommandHandler<ProcessarRecebimentoCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ILancamentos _lancamentos;
        private readonly ProcessarRecebimento _processarRecebimento;
        public ProcessarRecebimentoCommandHandler(ProcessarRecebimento processarRecebimento, IEventBus eventBus, ILancamentos lancamentos)
        {
            _processarRecebimento = processarRecebimento;
            _eventBus = eventBus;
            _lancamentos = lancamentos;
        }

        public async Task Handle(CommandContext<ProcessarRecebimentoCommand> context)
        {
            var lancamento = _lancamentos.FindById(context.Command.LancamentoId);
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