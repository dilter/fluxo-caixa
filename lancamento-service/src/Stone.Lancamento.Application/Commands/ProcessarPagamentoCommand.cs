using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{
    using Domain.Lancamentos.Entities;    
    public class ProcessarPagamentoCommand : ICommand
    {
        public Lancamento Input { get; set; }
        public ProcessarPagamentoCommand(Lancamento input)
        {
            this.Input = input;
        }
    }
}