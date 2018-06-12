using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Lancamento.Application.Queries.Outputs;
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

                c.CreateMap<Pagamento, SaidaOutput>()
                    .ForMember(x => x.Data, m => m.MapFrom(y => y.Em.ToString("dd/MM/yyyy")))
                    .ForMember(x => x.Valor, m => m.MapFrom(y => y.Valor.ToCurrencyString()));
                
                c.CreateMap<Pagamento, EncargoOutput>()
                    .ForMember(x => x.Data, m => m.MapFrom(y => y.Em.ToString("dd/MM/yyyy")))
                    .ForMember(x => x.Valor, m => m.MapFrom(y => y.Encargos.ToCurrencyString()));
                
                c.CreateMap<Recebimento, EntradaOutput>()
                    .ForMember(x => x.Data, m => m.MapFrom(y => y.Em.ToString("dd/MM/yyyy")))
                    .ForMember(x => x.Valor, m => m.MapFrom(y => y.Valor.ToCurrencyString()));

                c.CreateMap<Consolidacao, FluxoCaixaOutput>()
                    .ForMember(x => x.Data, m => m.MapFrom(y => y.Data.ToString("dd/MM/yyyy")))
                    .ForMember(x => x.Entradas, m => m.MapFrom(y => y.Recebimentos.Where(x => x.Encargos == 0)))
                    .ForMember(x => x.Encargos, m => m.MapFrom(y => y.Pagamentos.Where(x => x.Encargos != 0)))
                    .ForMember(x => x.Saidas, m => m.MapFrom(y => y.Pagamentos.Where(x => x.Encargos == 0)));
            });
        }
    }
}