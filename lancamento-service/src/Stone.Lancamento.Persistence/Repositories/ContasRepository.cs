using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Repositories
{
    public class ContasRepository : Repository<ContaBancaria>, IContas
    {
        public ContasRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public ContaBancaria GetByCnpj(Cnpj cnpj)
        {
            return this.GetAll()
                .Include(x => x.Empresa)                
                .FirstOrDefault(x => x.Cnpj.Equals(cnpj));
        }

        public ContaBancaria GetByNumero(string numero)
        {
            return this.GetAll()
                .Include(x => x.Empresa)                
                .FirstOrDefault(x => x.Numero == numero);
        }
    }
}