using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Entities;
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

        public override IQueryable<ContaBancaria> GetAll()
        {
            return base.GetAll()
                .Include(x => x.Empresa);
        }

        public ContaBancaria GetByCnpj(Cnpj cnpj)
        {
            return this.GetAll()
                .Include(x => x.Empresa)                
                .FirstOrDefault(x => x.Empresa.Cnpj.Equals(cnpj));
        }

        public ContaBancaria GetByNumero(string numero)
        {
            return this.GetAll()
                .Include(x => x.Empresa)                
                .FirstOrDefault(x => x.Numero == numero);
        }

        public decimal GetSaldoById(Guid contaId)
        {            
            var sumRecebimentos = _unitOfWork.FindAll<Recebimento>().Sum(x => x.Valor);
            var sumPagamentos = _unitOfWork.FindAll<Pagamento>().Sum(x => x.Valor);
            return sumRecebimentos - sumPagamentos;
        }
    }
}