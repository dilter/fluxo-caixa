using System;
using System.Threading.Tasks;
using Stone.Lancamento.Application.Events;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Extensions;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    using Domain.Lancamentos.Entities;
    
    public class CriarLancamentoCommandHandler : IAsyncCommandHandler<CriarLancamentoCommand>,
        IAsyncEventHandler<LancamentoProcessadoEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly ILancamentos _lancamentos;
        public CriarLancamentoCommandHandler(ILancamentos lancamentos, ICommandBus commandBus)
        {
            _lancamentos = lancamentos;
            _commandBus = commandBus;
        }

        private async Task EnviarParaProcessamento(Lancamento input)
        {
            switch (input.Tipo)
            {
                case TipoLancamento.Pagamento:
                    await _commandBus.SendAsync(new ProcessarPagamentoCommand(input));
                    break;
                case TipoLancamento.Recebimento:
                    await _commandBus.SendAsync(new ProcessarRecebimentoCommand(input));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public async Task Handle(CommandContext<CriarLancamentoCommand> context)
        {
            try
            {
                var input = context.Command.Input;
                var lancamento = input.MapTo<Lancamento>();
                
                _lancamentos.Add(lancamento);
                
                await this.EnviarParaProcessamento(lancamento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Handle(EventContext<LancamentoProcessadoEvent> context)
        {
            
        }
    }
}