using System;
using System.Globalization;
using FluentValidation;
using Stone.Lancamento.Application.Commands.Inputs;

namespace Stone.Lancamento.WebApi.Validation
{
    public class ConsolidacaoValidator : AbstractValidator<ConsolidacaoInput>
    {
        public ConsolidacaoValidator()
        {
            RuleFor(r => r.Data)
                .Must(m =>
                {
                    try
                    {
                        DateTime.Parse(m, new CultureInfo("pt-BR"));
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage("A data de consolidação deve estar no formato dd/MM/yyyy");
        }
    }
}