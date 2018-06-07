using System;
using System.Globalization;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Application.Commands.Inputs;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Extensions;

namespace Stone.Lancamento.Application
{
    using Domain.Lancamentos.Entities;
    
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<LancamentoInput, Lancamento>()
                    .ForMember(x => x.Valor, m => m.ResolveUsing(y =>
                    {                        
                        y.ValorLancamento.TryParseFromRealCurrency(out var valor);
                        return valor;
                    }))                    
                    .ForMember(x => x.Em, m => m.MapFrom(y => DateTime.Parse(y.DataDeLancamento, new CultureInfo("pt-BR"))))                    
                    .ForMember(x => x.ContaDestino, m => m.ResolveUsing(x =>
                    {
                        var entity = new ContaBancaria
                        {
                            Banco = x.BancoDestino,                            
                            Numero = x.ContaDestino,                            
                            Tipo = x.TipoDeConta,
                        };

                        if (!string.IsNullOrEmpty(x.Cnpj))
                        {
                            entity.Cnpj = x.Cnpj;
                        }
                        
                        if (!string.IsNullOrEmpty(x.Cpf))
                        {
                            entity.Cpf = x.Cpf;
                        }
                        
                        return entity;
                    }));
            });
            
            return services;
        }
    }
}