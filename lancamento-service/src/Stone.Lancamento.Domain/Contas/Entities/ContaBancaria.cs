using System;
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
        public decimal Limite { get; protected set; }

        public ContaBancaria()
        {
            
        }
        
        public ContaBancaria(Empresa empresa, Banco banco, string numero, TipoConta tipo, decimal limite = 0)
        {
            this.Empresa = empresa;
            this.Banco = banco;
            this.Numero = numero;
            this.Tipo = tipo;
            this.Limite = limite;
        }

        private bool HasLimiteDisponivel(decimal valor)
        {
            return this.Limite >= valor;
        }

        public void EfetuarRecebimento(Recebimento recebimento)
        {
            this.Limite += recebimento.Valor;
        }
        
        public void EfetuarPagamento(Pagamento pagamento)
        {
            if (!this.HasLimiteDisponivel(pagamento.Valor))
            {
                throw new Exception("Limite indisponível");                    
            }
            this.Limite -= pagamento.Valor;
        }
    }
}