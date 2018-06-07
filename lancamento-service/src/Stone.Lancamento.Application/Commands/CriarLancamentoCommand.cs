using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{
    public class CriarLancamentoCommand : ICommand
    {
        public LancamentoInput Input { get; set; }
        public CriarLancamentoCommand(LancamentoInput input)
        {
            this.Input = input;
        }
    }
}