using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Application;
using Stone.Lancamento.Application.Commands;
using Stone.Lancamento.Domain.Contas.Services;
using Stone.Lancamento.Domain.Lancamentos.Entities;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Lancamento.Persistence.Configuration;
using Stone.Lancamento.Persistence.Extensions;
using Stone.Sdk.Extensions;

namespace Stone.Lancamento.Consolidador
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services
                .AddPersistenceEfContext<LancamentosDbContext>(Configuration)
                .AddScoped<ProcessarPagamento>()
                .AddScoped<ProcessarRecebimento>()                
                .AddScoped<ConsolidarLancamentos>()                
                .AddScoped<CalcularSaldo>()
                .AddScoped<CalcularEncargos>()       
                .AddRepositories();
            
            services.AddMediatR(typeof(ReceberLancamentoCommand).Assembly);
            services.AddMessageBroker(Configuration);
            services.AddElastisearch(Configuration, new []{ typeof(Pagamento) });
            services.AddApplicationMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCommandHandlerFor<ProcessarPagamentoCommand>();
            app.UseCommandHandlerFor<ProcessarRecebimentoCommand>();
            app.UseCommandHandlerFor<ConsolidarLancamentosCommand>();
        }
    }
}
