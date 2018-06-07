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
            
            RuleFor(r => r.ValorLancamento)
                .Must(v => v.TryParseFromRealCurrency(out var valor))
                .WithMessage("O formato deve ser: R$0,00");
        }
    }
}