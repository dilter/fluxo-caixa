using System;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{
    public class ProcessarRecebimentoCommand : ICommand
    {
        public Guid LancamentoId { get; set; }        
        public ProcessarRecebimentoCommand(Guid lancamentoId)
        {
            this.LancamentoId = lancamentoId;            
        }
    }
}