using System.Collections.Generic;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Contas.Entities
{
    public class ContaBancaria : Entity
    {
        public Empresa Empresa { get; set; }
        public Banco Banco { get; set; }
        public string Numero { get; set; }
        public TipoConta Tipo { get; set; }
        public Cpf Cpf { get; set; }
        public Cnpj Cnpj { get; set; }
        public decimal Limite { get; set; }
        private ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
        private ICollection<Recebimento> Recebimentos { get; set; } = new List<Recebimento>();
    }
}