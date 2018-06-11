namespace Stone.Lancamento.Domain.Contas.ValueObjects
{
    public class SaldoContaBancaria
    {
        public decimal Valor { get; set; }
        public decimal ValorComLimite { get; set; }
        public UtilizacaoLimite UtilizacaoLimite { get; set; }

        public bool Has(decimal valor)
        {
            return this.ValorComLimite >= valor;
        }

        public bool IsNegativo()
        {
            return this.Valor < 0;
        }
    }
}