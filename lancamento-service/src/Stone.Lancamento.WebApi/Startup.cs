using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Stone.Lancamento.Application;
using Stone.Lancamento.Application.Commands;
using Stone.Lancamento.Application.Events;
using Stone.Lancamento.Domain.Lancamentos.Services;
using Stone.Lancamento.Persistence.Configuration;
using Stone.Lancamento.Persistence.Extensions;
using Stone.Lancamento.WebApi.Validation;
using Stone.Sdk.Extensions;
using Stone.Sdk.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace Stone.Lancamento.WebApi
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
                .AddMvc(options =>
                {
                    options.Filters.Add(new UnitOfWorkFilter());
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LancamentoValidator>())                
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });

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
            
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Lançamento Service", Version = "v1" });
                    c.DescribeAllEnumsAsStrings();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSubcriberFor<LancamentoProcessadoEvent>();
            
            app.UseCommandHandlerFor<CriarLancamentoCommand>();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
            
            // Sample data!
            //app.SeedInMemoryData();
        }
    }
}
