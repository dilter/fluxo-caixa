using System;
using System.Threading.Tasks;
using Stone.Lancamento.Application.Events;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    public class ProcessarPagamentoCommandHandler : IAsyncCommandHandler<ProcessarPagamentoCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ProcessarPagamento _processarPagamento;
        public ProcessarPagamentoCommandHandler(ProcessarPagamento processarPagamento, IEventBus eventBus)
        {
            _processarPagamento = processarPagamento;
            _eventBus = eventBus;
        }

        public async Task Handle(CommandContext<ProcessarPagamentoCommand> context)
        {
            var lancamento = context.Command.Input;
            try
            {                
                await _processarPagamento.Apply(lancamento);
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id), context);
            }
            catch (Exception e)
            {
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id, e), context);
            }
        }
    }
}