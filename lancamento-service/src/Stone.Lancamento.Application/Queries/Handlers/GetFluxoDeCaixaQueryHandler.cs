using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Application.Queries.Outputs;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Contas.Services;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Extensions;

namespace Stone.Lancamento.Application.Queries.Handlers
{
    public class GetFluxoDeCaixaQueryHandler : IAsyncRequestHandler<GetFluxoDeCaixaQuery, IEnumerable<FluxoCaixaOutput>>
    {        
        private readonly IContas _contas;
        private readonly ILancamentos _lancamentos;
        private readonly CalcularSaldo _calcularSaldo;
        private readonly IConsolidacoes _consolidacoes;

        public GetFluxoDeCaixaQueryHandler(IContas contas, ILancamentos lancamentos, CalcularSaldo calcularSaldo, IConsolidacoes consolidacoes)
        {
            _contas = contas;
            _lancamentos = lancamentos;
            _calcularSaldo = calcularSaldo;
            _consolidacoes = consolidacoes;
        }

        public Task<IEnumerable<FluxoCaixaOutput>> Handle(GetFluxoDeCaixaQuery query)
        {
            var consolidacoes = _consolidacoes
                .FindAll(new Consolidacao.ByMes(query.Filter.Mes).And(new Consolidacao.ByAno(query.Filter.Ano)))
                .Include(x => x.Pagamentos)
                .Include(x => x.Recebimentos)
                .ToList();            
            var fluxoDeCaixa = consolidacoes.Select(c => c.MapTo<FluxoCaixaOutput>());                                    
            return Task.FromResult(fluxoDeCaixa.AsEnumerable());
        }
    }
}