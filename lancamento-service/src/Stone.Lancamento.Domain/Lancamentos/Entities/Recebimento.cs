using System;
using System.Linq.Expressions;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Recebimento : Entity
    {
        public Lancamento Lancamento { get; set; }
        public decimal Valor { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public decimal Encargos { get; set; }
        public DateTime Em { get; set; }
        public Consolidacao Consolidacao { get; set; }
        
        public class ByContaBancaria : Specification<Recebimento>
        {
            public ContaBancaria ContaBancaria { get; }
            public ByContaBancaria(ContaBancaria contaBancaria)
            {
                this.ContaBancaria = contaBancaria;
            }
            public override Expression<Func<Recebimento, bool>> IsSatisfiedBy()
            {
                return c => c.ContaBancaria.Id == this.ContaBancaria.Id;
            }
        }
        
        public class ByData : Specification<Recebimento>
        {
            public DateTime Data { get; set; }
            public ByData(DateTime data)
            {
                this.Data = data;
            }
            public override Expression<Func<Recebimento, bool>> IsSatisfiedBy()
            {
                return c => c.Em.Date == this.Data.Date;
            }
        }
        
        public class NaoConsolidado : Specification<Recebimento>
        {
            public override Expression<Func<Recebimento, bool>> IsSatisfiedBy()
            {
                return c => c.Consolidacao == null;
            }
        }
    }
}