using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Repositories
{
    using Domain.Lancamentos.Entities;
    
    public class LancamentosRepository : Repository<Lancamento>, ILancamentos
    {
        public LancamentosRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
         
        }
    }
}