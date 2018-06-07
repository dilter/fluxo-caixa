using System;
using System.Globalization;
using FluentValidation;
using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Extensions;

namespace Stone.Lancamento.WebApi.Validation
{
    public class LancamentoValidator : AbstractValidator<LancamentoInput>
    {
        public LancamentoValidator()
        {            
            RuleFor(r => r.BancoDestino).IsInEnum();
            RuleFor(r => r.TipoDeConta).IsInEnum();
            RuleFor(r => r.Tipo).IsInEnum();
            RuleFor(r => r.Cnpj)                
                .Must(m =>
                {
                    try
                    {
                        Cnpj cnpj = m;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .When(x => string.IsNullOrEmpty(x.Cpf))
                .WithMessage("Informe o CNPJ no formato 00.000.000/0001-00.")
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.Cpf))
                .WithMessage("O CNPJ deve ser informado sem preenchimento do CPF.");
            
            RuleFor(r => r.Cpf)                      
                .Must(m =>
                {
                    try
                    {
                        Cpf cpf = m;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }                    
                })                
                .When(x => string.IsNullOrEmpty(x.Cnpj))    
                .WithMessage("Informe o CPF no formato 000.000.000-00.")                
                .When(x=> !string.IsNullOrEmpty(x.Cnpj))                
                .WithMessage("O CPF deve ser informado sem preenchimento do CNPJ.");

            RuleFor(r => r.DataDeLancamento)
                .Must(m =>
                {
                    try
                    {
                        var data = DateTime.Parse(m, new CultureInfo("pt-BR"));
                        return data.Date >= DateTime.Now.Date;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage($"A data de lançamento deve maior igual a {DateTime.Now.Date:dd/MM/yyyy} e deve estar no formato dd/MM/yyyy");
            
            RuleFor(r => r.ValorLancamento)
                .Must(v => v.TryParseFromRealCurrency(out var valor))
                .WithMessage("O formato deve ser: R$0,00");
        }
    }
}