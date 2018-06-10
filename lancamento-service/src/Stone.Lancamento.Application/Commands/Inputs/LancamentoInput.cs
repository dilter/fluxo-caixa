using System.ComponentModel;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Application.Commands.Inputs
{
    public class LancamentoInput
    {
        public TipoLancamento Tipo { get; set; }
        public string Descricao { get; set; }
        public string ContaDestino { get; set; }
        public Banco BancoDestino { get; set; }
        public TipoConta TipoDeConta { get; set; }                
        public string Cnpj { get; set; }
        [DefaultValue("R$ 0,00")]
        public string ValorLancamento { get; set; }         
        public string DataDeLancamento { get; set; }
    }
}