using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Contas.Repositories
{
    public interface IContas : IRepository<ContaBancaria>
    {
        ContaBancaria GetByCnpj(Cnpj cnpj);
        ContaBancaria GetByNumero(string numero);
    }
}