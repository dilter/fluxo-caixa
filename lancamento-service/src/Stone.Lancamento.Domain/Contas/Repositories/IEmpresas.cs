using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Contas.Repositories
{
    public interface IEmpresas : IRepository<Empresa>
    {
        Empresa GetByCnpj(Cnpj cnpj);
    }
}