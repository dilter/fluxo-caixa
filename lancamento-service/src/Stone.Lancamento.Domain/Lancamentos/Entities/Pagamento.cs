using System;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Pagamento : Entity
    {
        public decimal Valor { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        public decimal Encargos { get; set; }
        public DateTime Em { get; set; }             
    }
}