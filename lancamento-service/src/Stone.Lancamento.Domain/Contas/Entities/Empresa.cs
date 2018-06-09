using System.Collections.Generic;
using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Contas.Entities
{
    public class Empresa : Entity
    {
        public Cnpj Cnpj { get; set; }        
        public ICollection<ContaBancaria> ContasBancarias { get; protected set; }

        public Empresa()
        {
            this.ContasBancarias = new List<ContaBancaria>();
        }
        
        public void AdicionarContaBancaria(ContaBancaria contaBancaria)
        {            
            contaBancaria.Cnpj = this.Cnpj;
            this.ContasBancarias.Add(contaBancaria);
        }
    }
}