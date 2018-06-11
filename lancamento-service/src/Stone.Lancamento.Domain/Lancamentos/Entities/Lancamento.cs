using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Lancamento : Entity
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string ContaDestino { get; set; }
        public TipoConta TipoConta { get; set; }
        public Cnpj Cnpj { get; set; }        
        public DateTime Em { get; set; }
        public TipoLancamento Tipo { get; set; }
        public SituacaoLancamento Situacao { get; set; }
        public string MensagemProcessamento { get; set; }

        public Lancamento()
        {
            this.Situacao = SituacaoLancamento.Recebido;
            this.CreationTime = DateTime.Now;
        }
        
        public class ByData : Specification<Lancamento>
        {
            public DateTime Data { get; }
            public ByData(DateTime data)
            {
                this.Data = data;
            }
            public override Expression<Func<Lancamento, bool>> IsSatisfiedBy()
            {
                return c => c.Em.Date == this.Data.Date;
            }
        }
        
        public class Recebidos : Specification<Lancamento>
        {
            public override Expression<Func<Lancamento, bool>> IsSatisfiedBy()
            {
                return c => c.Situacao == SituacaoLancamento.Recebido;
            }
        }
    }
}