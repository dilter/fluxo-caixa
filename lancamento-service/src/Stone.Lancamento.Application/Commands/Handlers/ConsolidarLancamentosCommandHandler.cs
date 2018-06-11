using System.Linq;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Sdk.Extensions;
using Stone.Sdk.Messaging;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Application.Commands.Handlers
{
    using Domain.Lancamentos.Entities;
    
    public class ConsolidarLancamentosCommandHandler : IAsyncCommandHandler<ConsolidarLancamentosCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConsolidacoes _consolidacoes;
        private readonly IContas _contas;
        private readonly ConsolidarLancamentos _consolidarLancamentos;
        public ConsolidarLancamentosCommandHandler(IConsolidacoes consolidacoes, ConsolidarLancamentos consolidarLancamentos, IContas contas, IUnitOfWork unitOfWork)
        {
            _consolidacoes = consolidacoes;
            _consolidarLancamentos = consolidarLancamentos;
            _contas = contas;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CommandContext<ConsolidarLancamentosCommand> context)
        {            
            var data = context.Command.Input.Data.ToLocalDateTime();
            var contaBancariaId = context.Command.Input.ContaBancariaId;
            var consolidacao = _consolidacoes.FindAll(new Consolidacao.ByData(data));
            if (!consolidacao.Any())
            {
                var contaBancaria = _contas.FindById(contaBancariaId);                 
                await _consolidarLancamentos.Apply(contaBancaria, data);
                _unitOfWork.Commit();
            }       
        }
    }
}