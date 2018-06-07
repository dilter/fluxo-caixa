using System;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Extensions;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    using Domain.Lancamentos.Entities;
    
    public class CriarLancamentoCommandHandler : IAsyncCommandHandler<CriarLancamentoCommand>
    {
        private readonly ICommandBus _commandBus;
        private readonly ILancamentos _lancamentos;
        public CriarLancamentoCommandHandler(ILancamentos lancamentos, ICommandBus commandBus)
        {
            _lancamentos = lancamentos;
            _commandBus = commandBus;
        }

        private async Task EnviarProcessamento(Lancamento lancamento)
        {
            switch (lancamento.Tipo)
            {
                case TipoLancamento.Pagamento:
                    await _commandBus.SendAsync(new ProcessarPagamentoCommand());
                    break;
                case TipoLancamento.Recebimento:
                    await _commandBus.SendAsync(new ProcessarRecebimentoCommand());
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
                
                await this.EnviarProcessamento(lancamento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}