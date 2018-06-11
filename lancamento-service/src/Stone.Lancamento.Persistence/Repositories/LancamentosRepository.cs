using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Repositories
{
    using Domain.Lancamentos.Entities;
    
    public class LancamentosRepository : Repository<Lancamento>, ILancamentos
    {
        public LancamentosRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
         
        }

        public Pagamento AddPagamento(Pagamento pagamento)
        {
            var entity = _unitOfWork.Add(pagamento);
            _unitOfWork.Commit();
            return entity;
        }

        public Recebimento AddRecebimento(Recebimento recebimento)
        {
            var entity = _unitOfWork.Add(recebimento);
            _unitOfWork.Commit();
            return entity;
        }

        public IQueryable<Lancamento> GetAllRecebidosByData(DateTime data)
        {
            return this.GetAll()
                .Where(x => x.Situacao == SituacaoLancamento.Recebido)
                .Where(x => x.Em.Date == data.Date);
        }

        public IQueryable<Pagamento> FindAllPagamentos(ISpecification<Pagamento> specification = null)
        {
            var all = _unitOfWork.FindAll<Pagamento>();
            if (specification != null)
            {
                all = all.Where(specification.IsSatisfiedBy());
            }            
            return all.Include(x => x.ContaBancaria);
        }

        public IQueryable<Recebimento> FindAllRecebimentos(ISpecification<Recebimento> specification = null)
        {
            var all = _unitOfWork.FindAll<Recebimento>();
            if (specification != null)
            {
                all = all.Where(specification.IsSatisfiedBy());
            }            
            return all.Include(x => x.ContaBancaria);
        }
    }
}