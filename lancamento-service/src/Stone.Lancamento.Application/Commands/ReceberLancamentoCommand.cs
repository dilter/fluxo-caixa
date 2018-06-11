using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{
    public class ReceberLancamentoCommand : ICommand
    {
        public LancamentoInput Input { get; set; }
        public ReceberLancamentoCommand(LancamentoInput input)
        {
            this.Input = input;
        }      
    }
}