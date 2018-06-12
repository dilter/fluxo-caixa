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
    public class ProcessarRecebimentoCommandHandler : IAsyncCommandHandler<ProcessarRecebimentoCommand>
    {
        private readonly IEventBus _eventBus;
        private readonly ILancamentos _lancamentos;
        private readonly IConsolidacoes _consolidacoes;
        private readonly ProcessarRecebimento _processarRecebimento;
        private readonly IUnitOfWork _unitOfWork;
        public ProcessarRecebimentoCommandHandler(ProcessarRecebimento processarRecebimento, IEventBus eventBus, ILancamentos lancamentos, IConsolidacoes consolidacoes, IUnitOfWork unitOfWork)
        {
            _processarRecebimento = processarRecebimento;
            _eventBus = eventBus;
            _lancamentos = lancamentos;
            _consolidacoes = consolidacoes;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CommandContext<ProcessarRecebimentoCommand> context)
        {
            var lancamento = _lancamentos.FindById(context.Command.LancamentoId);
            try
            {              
                var consolidacao = _consolidacoes.FindAll(new Consolidacao.ByData(lancamento.Em)).FirstOrDefault();
                if (consolidacao == null)
                {
                    _consolidacoes.Add(new Consolidacao(lancamento.Em));
                }                
                await _processarRecebimento.Apply(consolidacao, lancamento);
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id));
            }
            catch (Exception e)
            {
                await _eventBus.PublishAsync(new LancamentoProcessadoEvent(lancamento.Id, e));
            }
            _unitOfWork.Commit();
        }
    }
}