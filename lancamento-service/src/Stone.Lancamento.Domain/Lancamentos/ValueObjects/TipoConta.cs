using System.ComponentModel;

namespace Stone.Lancamento.Domain.Lancamentos.ValueObjects
{
    public enum TipoConta
    {
        [Description("Conta Corrente")]
        ContaCorrente,
        [Description("Poupança")]
        Poupanca
    }
}