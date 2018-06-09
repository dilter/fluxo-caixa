using System;
using Microsoft.AspNetCore.Builder;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.WebApi
{
    public static class ApplicationBuilderExtensions
    {
        public static void SeedData(this IApplicationBuilder builder)
        {
            var lancamentosRepository = ((ILancamentos)builder.ApplicationServices.GetService(typeof(ILancamentos)));
            var empresasRepository = ((IEmpresas)builder.ApplicationServices.GetService(typeof(IEmpresas)));
            var contasRepository = ((IContas)builder.ApplicationServices.GetService(typeof(IContas)));
            
            var uow = ((IUnitOfWork)builder.ApplicationServices.GetService(typeof(IUnitOfWork)));

            var empresa = empresasRepository.Add(new Empresa()
            {
                Cnpj = "15.381.215/0001-77",                
            });
            
            empresa.AdicionarContaBancaria(new ContaBancaria()
            {
                Banco = Banco.Santander,
                Limite = 20000,
                Numero = "13000715-7",
                Tipo = TipoConta.ContaCorrente,                                    
            });

            uow.Commit();
            
        }        
    }
}