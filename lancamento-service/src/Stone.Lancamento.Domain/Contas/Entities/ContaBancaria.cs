using System;
using System.Linq.Expressions;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;
using Stone.Sdk.Domain.Specification;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Contas.Entities
{
    public class ContaBancaria : Entity
    {
        public Empresa Empresa { get; set; }
        public Banco Banco { get; set; }
        public string Numero { get; set; }
        public TipoConta Tipo { get; set; }        
        public decimal Limite { get; protected set; }
        public decimal TaxaUtilizacaoLimite { get; set; }

        public ContaBancaria()
        {
            
        }
        
        public ContaBancaria(Empresa empresa, Banco banco, string numero, TipoConta tipo, decimal limite = 0)
        {
            this.Empresa = empresa;
            this.Banco = banco;
            this.Numero = numero;
            this.Tipo = tipo;
            this.Limite = limite;
        }

        public class ByNumero : Specification<ContaBancaria>
        {
            public string Numero { get; set; }
            public ByNumero(string numero)
            {
                this.Numero = numero;
            }
            public override Expression<Func<ContaBancaria, bool>> IsSatisfiedBy()
            {
                return c => c.Numero == this.Numero;
            }
        }
    }
}