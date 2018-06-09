using System.Threading.Tasks;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Services
{
    using Domain.Lancamentos.Entities;
    
    public class ProcessarRecebimento : IDomainService
    {        
        public async Task Apply(Lancamento lancamento)
        {
            
        }
        
        public void Dispose()
        {
            
        }
    }
}