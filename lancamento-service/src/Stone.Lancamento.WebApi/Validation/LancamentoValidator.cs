using System;
using System.Globalization;
using FluentValidation;
using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Sdk.Domain;
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
                .WithMessage("Informe o CNPJ no formato 00.000.000/0001-00.");
                
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