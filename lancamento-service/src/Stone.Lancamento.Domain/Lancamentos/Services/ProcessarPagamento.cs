using System;
using System.Linq;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Contas.Services;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    using Domain.Lancamentos.Entities;
    
    public class ProcessarPagamento : IDomainService
    {        
        private readonly IContas _contas;
        private readonly ILancamentos _lancamentos;
        private readonly CalcularSaldo _calcularSaldo;
        
        public ProcessarPagamento(IContas contas, ILancamentos lancamentos, CalcularSaldo calcularSaldo)
        {
            _contas = contas;
            _lancamentos = lancamentos;
            _calcularSaldo = calcularSaldo;
        }

        public async Task<Pagamento> Apply(Consolidacao consolidacao, Lancamento lancamento)
        {
            try
            {                  
                var contaBancaria = _contas.FindAll(new ContaBancaria.ByNumero(lancamento.ContaDestino)).FirstOrDefault();
                var saldo = _calcularSaldo.Apply(contaBancaria);                
                
                if (contaBancaria == null)
                {
                    throw new Exception("Conta Bancária inválida");
                }
                
                if (!contaBancaria.Tipo.Equals(lancamento.TipoConta))
                {
                    throw new Exception("Tipo de conta inválido");
                }   
                
                var pagamento = new Pagamento(lancamento, contaBancaria);
                
                if (!saldo.Has(pagamento.Valor))
                {
                    throw new Exception("Não há limite disponível");
                }                
                
                _lancamentos.AddPagamento(pagamento);                
                
                consolidacao.Pagamentos.Add(pagamento);
                
                return pagamento;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
        
        public void Dispose()
        {
            
        }
    }
}