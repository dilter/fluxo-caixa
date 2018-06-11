using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Repositories
{
    public class ConsolidacoesRepository : Repository<Consolidacao>, IConsolidacoes
    {
        public ConsolidacoesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Consolidacao GetByData(DateTime data)
        {
            return this.GetAll()
                .Include(x => x.Pagamentos)
                .Include(x => x.Recebimentos)
                .FirstOrDefault(x => x.Data.Date == data.Date);
        }
    }
}