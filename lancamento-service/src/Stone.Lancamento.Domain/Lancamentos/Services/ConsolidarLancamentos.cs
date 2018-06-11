using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Services;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    public class ConsolidarLancamentos : IDomainService
    {
        private readonly ILancamentos _lancamentos;
        private readonly CalcularEncargos _calcularEncargos;
        private readonly IConsolidacoes _consolidacoes;
        public ConsolidarLancamentos(ILancamentos lancamentos, CalcularEncargos calcularEncargos, IConsolidacoes consolidacoes)
        {
            _lancamentos = lancamentos;
            _calcularEncargos = calcularEncargos;
            _consolidacoes = consolidacoes;
        }

        public async Task<Consolidacao> Apply(ContaBancaria contaBancaria, DateTime data)
        {
            var pagamentosNaoConsolidados = _lancamentos
                .FindAllPagamentos(new Pagamento.ByData(data).And(new Pagamento.NaoConsolidado()));
            
            var recebimentosNaoConsolidados = _lancamentos
                .FindAllRecebimentos(new Recebimento.ByData(data).And(new Recebimento.NaoConsolidado()));

            if (pagamentosNaoConsolidados.Any() || recebimentosNaoConsolidados.Any())
            {
                var consolidacao = new Consolidacao(data);
            
                consolidacao.Pagamentos.AddRange(pagamentosNaoConsolidados);
                consolidacao.Recebimentos.AddRange(recebimentosNaoConsolidados);
            
                var encargos = await _calcularEncargos.Apply(contaBancaria);
                if (encargos != null)
                {
                    consolidacao.Pagamentos.Add(encargos);    
                }            
            
                return _consolidacoes.Add(consolidacao);
            }
            return null;
        }
        
        public void Dispose()
        {
            
        }
    }
}