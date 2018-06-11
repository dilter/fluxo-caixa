using System;
using System.Linq;
using Stone.Sdk.Domain;
using Stone.Sdk.Domain.Specification;

namespace Stone.Lancamento.Domain.Lancamentos.Repositories
{
    using Entities;
    
    public interface ILancamentos : IRepository<Lancamento>
    {
        Pagamento AddPagamento(Pagamento pagamento);
        Recebimento AddRecebimento(Recebimento recebimento);

        IQueryable<Lancamento> GetAllRecebidosByData(DateTime data);
        IQueryable<Pagamento> FindAllPagamentos(ISpecification<Pagamento> specification = null);
        IQueryable<Recebimento> FindAllRecebimentos(ISpecification<Recebimento> specification = null);
    }
}