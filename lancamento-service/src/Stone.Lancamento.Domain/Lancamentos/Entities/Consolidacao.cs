using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Consolidacao : Entity
    {
        public DateTime Data { get; set; }
        public List<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
        public List<Recebimento> Recebimentos { get; set; } = new List<Recebimento>();
        public ProcessamentoConsolidacao Situacao { get; set; } 

        public Consolidacao()
        {
            
        }
        
        public Consolidacao(DateTime data)
        {
            this.Data = data;
        }
        
        public class ByMes : Specification<Consolidacao>
        {
            public int Mes { get; }
            public ByMes(int mes)
            {
                this.Mes = mes;
            }
            public override Expression<Func<Consolidacao, bool>> IsSatisfiedBy()
            {
                return c => c.Data.Month == this.Mes;
            }
        }
        
        public class ByAno : Specification<Consolidacao>
        {
            public int Ano { get; }
            public ByAno(int ano)
            {
                this.Ano = ano;
            }
            public override Expression<Func<Consolidacao, bool>> IsSatisfiedBy()
            {
                return c => c.Data.Year == this.Ano;
            }
        }
        
        public class ByData : Specification<Consolidacao>
        {
            public DateTime Data { get; }
            public ByData(DateTime data)
            {
                this.Data = data;
            }
            public override Expression<Func<Consolidacao, bool>> IsSatisfiedBy()
            {
                return c => c.Data.Date == this.Data.Date;
            }
        }
        
        public class NaoProcessada : Specification<Consolidacao>
        {
            public override Expression<Func<Consolidacao, bool>> IsSatisfiedBy()
            {
                return c => c.Situacao == ProcessamentoConsolidacao.NaoProcessada;
            }
        }
    }
}