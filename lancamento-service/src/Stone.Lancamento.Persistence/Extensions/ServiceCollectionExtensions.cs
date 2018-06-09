using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Persistence.Repositories;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceEfContext<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {            
            services.AddScoped<DbContext, TDbContext>();
            services.AddDbContext<TDbContext>(opt => opt.UseInMemoryDatabase(nameof(TDbContext)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {            
            services.AddScoped<ILancamentos, LancamentosRepository>();
            services.AddScoped<IEmpresas, EmpresasRepository>();
            services.AddScoped<IContas, ContasRepository>();
            
            return services;
        }
    }
}