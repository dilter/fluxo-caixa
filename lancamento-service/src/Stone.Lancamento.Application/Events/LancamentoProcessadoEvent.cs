using System;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Events
{    
    public class LancamentoProcessadoEvent : Event
    {
        public Guid LancamentoId { get; set; }
        public LancamentoProcessadoEvent()
        {
            
        }
        public LancamentoProcessadoEvent(Guid lancamentoId)
        {
            this.LancamentoId = lancamentoId;
        }

        public LancamentoProcessadoEvent(Guid lancamentoId, Exception exception)
            : base(exception)
        {
            this.LancamentoId = lancamentoId;
        }
    }
}