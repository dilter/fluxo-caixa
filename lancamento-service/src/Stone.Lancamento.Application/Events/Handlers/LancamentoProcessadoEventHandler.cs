using System.Threading.Tasks;
using Stone.Sdk.Messaging;

namespace Stone.Lancamento.Application.Events.Handlers
{
    public class LancamentoProcessadoEventHandler : IAsyncEventHandler<LancamentoProcessadoEvent>
    {
        public async Task Handle(EventContext<LancamentoProcessadoEvent> @event)
        {
            
        }
    }
}