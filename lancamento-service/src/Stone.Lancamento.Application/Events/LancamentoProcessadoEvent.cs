using System;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Events
{
    public class LancamentoProcessadoEvent : Event
    {
        public LancamentoProcessadoEvent()
        {
            
        }

        public LancamentoProcessadoEvent(Exception exception)
            : base(exception)
        {
            
        }
    }
}