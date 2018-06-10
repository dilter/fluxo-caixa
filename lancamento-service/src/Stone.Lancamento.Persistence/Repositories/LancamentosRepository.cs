using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
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

        public IQueryable<Lancamento> GetByData(DateTime data)
        {
            return this.GetAll()
                .Where(x => x.Em.Date == data.Date);
        }

        public IQueryable<Pagamento> GetAllPagamentos()
        {
            return _unitOfWork.FindAll<Pagamento>()
                .Include(x => x.ContaBancaria);
        }

        public IQueryable<Recebimento> GetAllRecebimentos()
        {
            return _unitOfWork.FindAll<Recebimento>()
                .Include(x => x.ContaBancaria);
        }
    }
}