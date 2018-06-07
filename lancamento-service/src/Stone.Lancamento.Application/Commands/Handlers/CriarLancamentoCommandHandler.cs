using System;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
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
                _lancamentos.Add(input.MapTo<Lancamento>());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}