using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Services;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    public class ConsolidarLancamentos : IDomainService
    {
        private readonly CalcularEncargos _calcularEncargos;
        public ConsolidarLancamentos(CalcularEncargos calcularEncargos)
        {
            _calcularEncargos = calcularEncargos;
        }

        public async Task<Consolidacao> Apply(Consolidacao consolidacao, ContaBancaria contaBancaria)
        {
            var encargos = await _calcularEncargos.Apply(contaBancaria);
            if (encargos != null)
            {
                consolidacao.Pagamentos.Add(encargos);    
            }
            consolidacao.Situacao = ProcessamentoConsolidacao.Processada;
            return consolidacao;
        }
        
        public void Dispose()
        {
            
        }
    }
}