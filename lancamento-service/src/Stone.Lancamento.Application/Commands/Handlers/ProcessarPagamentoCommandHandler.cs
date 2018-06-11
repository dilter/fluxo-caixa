using System;
using System.Threading.Tasks;
using Stone.Lancamento.Application.Events;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Sdk.Messaging;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    public class ProcessarPagamentoCommandHandler : IAsyncCommandHandler<ProcessarPagamentoCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly IIndexer _indexer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILancamentos _lancamentos;
        private readonly ProcessarPagamento _processarPagamento;
        public ProcessarPagamentoCommandHandler(ProcessarPagamento processarPagamento, IEventBus eventBus, IIndexer indexer, IUnitOfWork unitOfWork, ILancamentos lancamentos)
        {
            _processarPagamento = processarPagamento;
            _eventBus = eventBus;
            _indexer = indexer;
            _unitOfWork = unitOfWork;
            _lancamentos = lancamentos;
        }

        public async Task Handle(CommandContext<ProcessarPagamentoCommand> context)
        {
            var lancamento = _lancamentos.FindById(context.Command.LancamentoId);            
            try
            {                
                var pagamento = await _processarPagamento.Apply(lancamento);                
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id), context);
                // await _indexer.IndexAsync(pagamento);               
            }
            catch (Exception e)
            {
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id, e), context);
            }
            _unitOfWork.Commit();
        }
    }
}