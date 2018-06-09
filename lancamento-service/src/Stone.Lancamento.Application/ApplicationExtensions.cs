using System;
using System.Globalization;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Extensions;

namespace Stone.Lancamento.Application
{
    using Domain.Lancamentos.Entities;
    
    public static class ApplicationExtensions
    {
        public static void AddApplicationMappings(this IServiceCollection services)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<LancamentoInput, Lancamento>()
                    .ForMember(x => x.Valor, m => m.ResolveUsing(y =>
                    {
                        y.ValorLancamento.TryParseFromRealCurrency(out var valor);
                        return valor;
                    }))
                    .ForMember(x => x.Em, m => m.MapFrom(y => DateTime.Parse(y.DataDeLancamento, new CultureInfo("pt-BR"))));
            });
        }
    }
}