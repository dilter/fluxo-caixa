using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Consolidacao : Entity
    {
        public DateTime Data { get; set; }
        public List<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
        public List<Recebimento> Recebimentos { get; set; } = new List<Recebimento>();

        public Consolidacao()
        {
            
        }
        
        public Consolidacao(DateTime data)
        {
            this.Data = data;
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
    }
}