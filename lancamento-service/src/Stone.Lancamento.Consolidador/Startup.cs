using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stone.Lancamento.Application;
using Stone.Lancamento.Application.Commands;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Lancamento.Persistence.Configuration;
using Stone.Lancamento.Persistence.Extensions;
using Stone.Sdk.Extensions;

namespace Stone.Lancamento.Consolidador
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services
                .AddPersistenceEfContext<LancamentosDbContext>(Configuration)
                .AddScoped<ProcessarPagamento>()
                .AddScoped<ProcessarRecebimento>()
                .AddRepositories();
            
            services
                .AddMediatR(typeof(CriarLancamentoCommand).Assembly);

            services
                .AddMessageBroker(Configuration);

            services
                .AddApplicationMappings();
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
