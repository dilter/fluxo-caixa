using System;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Contas.ValueObjects;
using Stone.Sdk.Domain;

namespace Stone.Lancamento.Domain.Contas.Services
{
    public class CalcularSaldo : IDomainService
    {
        private readonly IContas _contas;
        public CalcularSaldo(IContas contas)
        {
            _contas = contas;
        }

        public SaldoContaBancaria Apply(ContaBancaria contaBancaria)
        {
            try
            {
                var saldo = _contas.GetSaldoById(contaBancaria.Id);
                var limiteConta = contaBancaria.Limite;
                var saldoNegativo = saldo < 0;
                var saldoContaBancaria = new SaldoContaBancaria
                {
                    Valor = saldo,
                    ValorComLimite = saldo + limiteConta,
                    UtilizacaoLimite = new UtilizacaoLimite
                    {
                        ValorDisponivel = saldoNegativo ? limiteConta - saldo : limiteConta,
                        ValorUtilizado = saldoNegativo ? limiteConta - saldo : 0,
                    },
                };
                return saldoContaBancaria;
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}