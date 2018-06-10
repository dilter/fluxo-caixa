using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Domain.Contas.Entities
{
    public class Empresa : Entity
    {
        public Cnpj Cnpj { get; set; }
        public string RazaoSocial { get; set; }
    }
}