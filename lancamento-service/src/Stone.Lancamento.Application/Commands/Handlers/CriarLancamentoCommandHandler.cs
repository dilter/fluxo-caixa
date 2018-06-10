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
    
    public class CriarLancamentoCommandHandler : IAsyncCommandHandler<CriarLancamentoCommand>
    {
        private readonly ILancamentos _lancamentos;
        public CriarLancamentoCommandHandler(ILancamentos lancamentos)
        {
            _lancamentos = lancamentos;
        }

        public async Task Handle(CommandContext<CriarLancamentoCommand> context)
        {
            try
            {
                var input = context.Command.Input;
                var lancamento = input.MapTo<Lancamento>();
                
                _lancamentos.Add(lancamento);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}