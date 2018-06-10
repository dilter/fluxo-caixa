using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Repositories
{
    public class EmpresasRepository : Repository<Empresa>, IEmpresas
    {
        public EmpresasRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Empresa GetByCnpj(Cnpj cnpj)
        {
            return this.GetAll()                                
                .FirstOrDefault(x => x.Cnpj.Equals(cnpj));
        }
        
        public override Empresa FindById(Guid id)
        {
            return this.GetAll()                                
                .FirstOrDefault(x => x.Id == id);
        }
    }
}