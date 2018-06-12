using System;
using System.Linq;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILancamentos _lancamentos;
        private readonly IConsolidacoes _consolidacoes;
        private readonly ProcessarPagamento _processarPagamento;        
        public ProcessarPagamentoCommandHandler(ProcessarPagamento processarPagamento, IEventBus eventBus, IUnitOfWork unitOfWork, ILancamentos lancamentos, IConsolidacoes consolidacoes)
        {
            _processarPagamento = processarPagamento;
            _eventBus = eventBus;            
            _unitOfWork = unitOfWork;
            _lancamentos = lancamentos;
            _consolidacoes = consolidacoes;
        }

        public async Task Handle(CommandContext<ProcessarPagamentoCommand> context)
        {
            var lancamento = _lancamentos.FindById(context.Command.LancamentoId);            
            try
            {
                var consolidacao = _consolidacoes.FindAll(new Consolidacao.ByData(lancamento.Em)).FirstOrDefault();
                if (consolidacao == null)
                {
                    consolidacao = _consolidacoes.Add(new Consolidacao(lancamento.Em));
                }                
                await _processarPagamento.Apply(consolidacao, lancamento);                
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id), context);                               
            }
            catch (Exception e)
            {
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id, e), context);
            }            
            _unitOfWork.Commit();
        }
    }
}