using System;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    using Domain.Lancamentos.Entities;
    
    public class ProcessarPagamento : IDomainService
    {        
        private readonly IContas _contas;
        private readonly ILancamentos _lancamentos;
        public ProcessarPagamento(IContas contas, ILancamentos lancamentos)
        {
            _contas = contas;
            _lancamentos = lancamentos;
        }

        public async Task Apply(Lancamento lancamento)
        {
            try
            {
                var contaBancaria = _contas.GetByNumero(lancamento.ContaDestino);
                if (contaBancaria == null)
                {
                    throw new Exception("Conta Bancária inválida");
                }
//                if (!contaBancaria.Empresa.Cnpj.Equals(lancamento.Cnpj))
//                {
//                    throw new Exception("Cnpj inválido");
//                }
                if (!contaBancaria.Tipo.Equals(lancamento.TipoConta))
                {
                    throw new Exception("Tipo de conta inválido");
                }   
                var pagamento = new Pagamento
                {
                    ContaBancaria = contaBancaria,
                    Em = lancamento.Em,
                    Valor = lancamento.Valor,                    
                };
                contaBancaria.EfetuarPagamento(pagamento);
                _lancamentos.AddPagamento(pagamento);
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