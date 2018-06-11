using Microsoft.AspNetCore.Builder;
using Stone.Lancamento.Domain.Contas.Entities;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.ValueObjects;
using Stone.Sdk.Domain;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.WebApi
{
    public static class ApplicationBuilderExtensions
    {
        public static void SeedInMemoryData(this IApplicationBuilder builder)
        {            
            var empresasRepository = ((IEmpresas)builder.ApplicationServices.GetService(typeof(IEmpresas)));
            var contasRepository = ((IContas)builder.ApplicationServices.GetService(typeof(IContas)));
            
            var uow = ((IUnitOfWork)builder.ApplicationServices.GetService(typeof(IUnitOfWork)));

            var empresa = empresasRepository.Add(new Empresa()
            {
                Cnpj = "15.381.215/0001-77",                
                RazaoSocial = "DILTER PORTO LADISLAU - ME",                
            });

            var contaBancaria = new ContaBancaria(empresa,
                Banco.Santander,
                "13000715-7",
                TipoConta.ContaCorrente,
                20000) {TaxaUtilizacaoLimite = (decimal) 0.83};
            contasRepository.Add(contaBancaria);
            
            uow.Commit();
            
        }        
    }
}