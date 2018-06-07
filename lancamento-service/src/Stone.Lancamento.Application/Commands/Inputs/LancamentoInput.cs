using System.ComponentModel;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;

namespace Stone.Lancamento.Application.Commands.Inputs
{
    public class LancamentoInput
    {
        public TipoLancamento Tipo { get; set; }
        public string Descricao { get; set; }
        public string ContaDestino { get; set; }
        public Banco BancoDestino { get; set; }
        public TipoConta TipoDeConta { get; set; }        
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        [DefaultValue("R$ 0,00")]
        public string ValorLancamento { get; set; }
        [DefaultValue("R$ 0,00")]
        public string Encargos { get; set; }        
    }


    public enum Banco
    {
        BancoDoBrasil,
        CEF,
        Bradesco,
        Santander,
        BancoDoNordeste
    }
    
    public enum TipoConta
    {
        [Description("Conta Corrente")]
        ContaCorrente,
        [Description("Poupança")]
        Poupanca
    }

    public enum TipoLancamento
    {
        Pagamento,
        Recebimento
    }
}