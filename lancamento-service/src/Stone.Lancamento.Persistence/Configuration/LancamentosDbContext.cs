using Microsoft.EntityFrameworkCore;
using Stone.Lancamento.Persistence.ModelConfiguration;

namespace Stone.Lancamento.Persistence.Configuration
{
    public class LancamentosDbContext : DbContext
    {
        public LancamentosDbContext(DbContextOptions builderOptions)
            : base(builderOptions)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                  
            modelBuilder.ApplyConfiguration(new LancamentoModelConfiguration());
            modelBuilder.ApplyConfiguration(new PagamentoModelConfiguration());
            modelBuilder.ApplyConfiguration(new RecebimentoModelConfiguration());
            modelBuilder.ApplyConfiguration(new ContaBancariaModelConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaModelConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}