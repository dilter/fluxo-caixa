using System;
using System.Linq.Expressions;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Pagamento : Entity
    {
        public Lancamento Lancamento { get; set; }
        public decimal Valor { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public decimal Encargos { get; set; }
        public DateTime Em { get; set; }
        public Consolidacao Consolidacao { get; set; }

        public Pagamento()
        {
            
        }

        public Pagamento(Lancamento lancamento, ContaBancaria contaBancaria)
        {
            this.Valor = lancamento.Valor;
            this.Em = lancamento.Em;
            this.Lancamento = lancamento;            
            this.ContaBancaria = contaBancaria;            
        }
        
        public class ByContaBancaria : Specification<Pagamento>
        {
            public ContaBancaria ContaBancaria { get; }
            public ByContaBancaria(ContaBancaria contaBancaria)
            {
                this.ContaBancaria = contaBancaria;
            }
            public override Expression<Func<Pagamento, bool>> IsSatisfiedBy()
            {
                return c => c.ContaBancaria.Id == this.ContaBancaria.Id;
            }
        }
        
        public class ByData : Specification<Pagamento>
        {
            public DateTime Data { get; set; }
            public ByData(DateTime data)
            {
                this.Data = data;
            }
            public override Expression<Func<Pagamento, bool>> IsSatisfiedBy()
            {
                return c => c.Em.Date == this.Data.Date;
            }
        }
        
        public class NaoConsolidado : Specification<Pagamento>
        {
            public override Expression<Func<Pagamento, bool>> IsSatisfiedBy()
            {
                return c => c.Consolidacao == null;
            }
        }
    }
}