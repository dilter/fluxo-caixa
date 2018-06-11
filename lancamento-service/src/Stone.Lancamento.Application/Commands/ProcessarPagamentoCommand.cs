using System;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Commands
{    
    public class ProcessarPagamentoCommand : ICommand
    {
        public Guid LancamentoId { get; set; }        
        public ProcessarPagamentoCommand(Guid lancamentoId)
        {
            this.LancamentoId = lancamentoId;            
        }
    }
}