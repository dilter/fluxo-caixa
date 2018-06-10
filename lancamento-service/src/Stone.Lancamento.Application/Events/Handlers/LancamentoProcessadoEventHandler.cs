using System.Threading.Tasks;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Messaging;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Application.Events.Handlers
{
    public class LancamentoProcessadoEventHandler : IAsyncEventHandler<LancamentoProcessadoEvent>
    {
        private readonly ILancamentos _lancamentos;
        private readonly IUnitOfWork _unitOfWork;        
        public LancamentoProcessadoEventHandler(IUnitOfWork unitOfWork, ILancamentos lancamentos)
        {
            _unitOfWork = unitOfWork;
            _lancamentos = lancamentos;
        }

        public async Task Handle(EventContext<LancamentoProcessadoEvent> context)
        {
            var @event = context.Event;
            var lancamentoId = @event.LancamentoId;
            var lancamento = _lancamentos.FindById(lancamentoId);

            if (@event.Type == EventType.Failure)
            {
                lancamento.Situacao = SituacaoLancamento.Rejeitado;
                lancamento.MensagemProcessamento = @event.Exception.Message;
            }
            else
            {
                lancamento.Situacao = SituacaoLancamento.Processado;
            }                        
            _unitOfWork.Commit();
        }
    }
}