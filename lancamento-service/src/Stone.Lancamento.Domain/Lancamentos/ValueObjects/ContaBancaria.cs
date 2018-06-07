namespace Stone.Lancamento.Domain.Lancamentos.ValueObjects
{
    public class ContaBancaria
    {
        public Banco Banco { get; set; }
        public string Numero { get; set; }
        public TipoConta Tipo { get; set; }
        public Cpf Cpf { get; set; }
        public Cnpj Cnpj { get; set; }
    }
}