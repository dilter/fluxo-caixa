using System.Threading.Tasks;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Contas.Services
{
    public class CalcularEncargos : IDomainService
    {
        private readonly CalcularSaldo _calcularSaldo;
        public CalcularEncargos(CalcularSaldo calcularSaldo)
        {
            _calcularSaldo = calcularSaldo;
        }

        public async Task<Pagamento> Apply(ContaBancaria contaBancaria)
        {
            var saldoContaBancaria = _calcularSaldo.Apply(contaBancaria);
            if (!saldoContaBancaria.IsNegativo()) return null;
            var valorEncargos = saldoContaBancaria.Valor * (contaBancaria.TaxaUtilizacaoLimite/100);
            return new Pagamento()
            {
                Descricao = "Cobrança de Taxas e Encargos",
                Encargos = valorEncargos,
                ContaBancaria = contaBancaria,
            };
        }
        
        public void Dispose()
        {
            
        }
    }
}