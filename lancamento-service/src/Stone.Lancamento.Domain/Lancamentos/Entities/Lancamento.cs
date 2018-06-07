using System;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Lancamentos.Entities
{
    public class Lancamento : Entity
    {
        public decimal Valor { get; set; }
        public ContaBancaria ContaDestino { get; set; }
        public decimal Encargos { get; set; }
        public DateTime Em { get; set; }
        public TipoLancamento Tipo { get; set; }

        public Lancamento()
        {
            this.ContaDestino = new ContaBancaria();
        }
    }
}