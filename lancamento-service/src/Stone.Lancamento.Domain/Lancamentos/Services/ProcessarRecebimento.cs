using System;
using System.Linq;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    using Domain.Lancamentos.Entities;
    
    public class ProcessarRecebimento : IDomainService
    {        
        private readonly IContas _contas;
        private readonly ILancamentos _lancamentos;

        public ProcessarRecebimento(IContas contas, ILancamentos lancamentos)
        {
            _contas = contas;
            _lancamentos = lancamentos;
        }

        public async Task<Recebimento> Apply(Consolidacao consolidacao, Lancamento lancamento)
        {
            var contaBancaria = _contas.FindAll(new ContaBancaria.ByNumero(lancamento.ContaDestino)).FirstOrDefault();                            
            if (contaBancaria == null)
            {
                throw new Exception("Conta Bancária inválida");
            }
                
            if (!contaBancaria.Tipo.Equals(lancamento.TipoConta))
            {
                throw new Exception("Tipo de conta inválido");
            }   
                
            var recebimento = new Recebimento
            {
                ContaBancaria = contaBancaria,
                Em = lancamento.Em,
                Valor = lancamento.Valor,                    
            };                            
                
            consolidacao.Recebimentos.Add(recebimento);
            
            _lancamentos.AddRecebimento(recebimento);                
                
            return recebimento;
        }
        
        public void Dispose()
        {
            
        }
    }
}