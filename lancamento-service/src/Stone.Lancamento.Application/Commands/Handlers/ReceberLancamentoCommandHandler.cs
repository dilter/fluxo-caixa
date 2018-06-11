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
    
    public class ReceberLancamentoCommandHandler : IAsyncCommandHandler<ReceberLancamentoCommand>
    {
        private readonly ILancamentos _lancamentos;
        private readonly ICommandBus _commandBus;
        public ReceberLancamentoCommandHandler(ILancamentos lancamentos, ICommandBus commandBus)
        {
            _lancamentos = lancamentos;
            _commandBus = commandBus;
        }

        private async Task EnviarParaProcessamento(Lancamento lancamento)
        {    
            _lancamentos.Add(lancamento);
            
            switch (lancamento.Tipo)
            {
                case TipoLancamento.Pagamento:
                    await _commandBus.SendAsync(new ProcessarPagamentoCommand(lancamento.Id));
                    break;
                case TipoLancamento.Recebimento:
                    await _commandBus.SendAsync(new ProcessarRecebimentoCommand(lancamento.Id));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }            
        }
        
        public async Task Handle(CommandContext<ReceberLancamentoCommand> context)
        {
            try
            {
                var input = context.Command.Input;
                var lancamento = input.MapTo<Lancamento>();

                await this.EnviarParaProcessamento(lancamento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}