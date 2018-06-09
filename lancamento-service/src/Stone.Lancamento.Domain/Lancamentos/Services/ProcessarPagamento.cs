using System;
using System.Threading.Tasks;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    using Domain.Lancamentos.Entities;
    
    public class ProcessarPagamento : IDomainService
    {
        private readonly IEmpresas _empresas;
        private readonly IContas _contas;
        public ProcessarPagamento(IEmpresas empresas, IContas contas)
        {
            _empresas = empresas;
            _contas = contas;
        }

        public async Task Apply(Lancamento lancamento)
        {
            try
            {
                var contaBancaria = _contas.GetByNumero(lancamento.ContaDestino);
                if (!contaBancaria.Empresa.Cnpj.Equals(lancamento.Cnpj))
                    throw new Exception("Cnpj inválido");
                
                
                
                var pagamento = new Pagamento();
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