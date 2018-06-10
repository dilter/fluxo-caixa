using System;
using System.Linq;
using System.Threading.Tasks;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Lancamentos.Repositories
{
    using Entities;
    
    public interface ILancamentos : IRepository<Lancamento>
    {
        Pagamento AddPagamento(Pagamento pagamento);
        Recebimento AddRecebimento(Recebimento recebimento);

        IQueryable<Lancamento> GetByData(DateTime data);
        IQueryable<Pagamento> GetAllPagamentos();
        IQueryable<Recebimento> GetAllRecebimentos();
    }
}