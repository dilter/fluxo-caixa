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

        public override IQueryable<Empresa> GetAll()
        {
            return base.GetAll()
                .Include(x => x.ContasBancarias);
        }

        public Empresa GetByCnpj(Cnpj cnpj)
        {
            return this.GetAll()
                .Include(x => x.ContasBancarias)                
                .FirstOrDefault(x => x.Cnpj.Equals(cnpj));
        }
        
        public override Empresa FindById(Guid id)
        {
            return this.GetAll()                
                .Include(x => x.ContasBancarias)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}