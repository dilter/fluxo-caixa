using System;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Repositories
{
    public interface IConsolidacoes : IRepository<Consolidacao>
    {
        Consolidacao GetByData(DateTime data);
    }
}