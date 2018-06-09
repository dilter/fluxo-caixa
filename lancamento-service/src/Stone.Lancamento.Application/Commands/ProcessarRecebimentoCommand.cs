using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{
    using Domain.Lancamentos.Entities;
    public class ProcessarRecebimentoCommand : ICommand
    {
        public Lancamento Input { get; set; }
        public ProcessarRecebimentoCommand(Lancamento input)
        {
            this.Input = input;
        }
    }
}