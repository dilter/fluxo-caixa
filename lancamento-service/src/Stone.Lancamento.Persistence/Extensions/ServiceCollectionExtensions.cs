using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Domain.Contas.Repositories;
using Stone.Lancamento.Domain.Lancamentos.Repositories;
using Stone.Lancamento.Persistence.Repositories;
using Stone.Sdk.Persistence;

namespace Stone.Lancamento.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceEfContext<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {            
            var configurationConnectionStringSection = configuration.GetSection("ConnectionStrings");
            services.AddScoped<DbContext, TDbContext>();
            services.AddDbContext<TDbContext>(opt => opt.UseSqlServer(configurationConnectionStringSection["DefaultConnection"]));
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