using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{
    public class ConsolidarLancamentosCommand : ICommand
    {
        public ConsolidacaoInput Input { get; set; }
        public ConsolidarLancamentosCommand(ConsolidacaoInput input)
        {
            this.Input = input;
        }
    }
}