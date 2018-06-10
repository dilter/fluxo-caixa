using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    using Domain.Lancamentos.Entities;

    public class LancamentoModelConfiguration : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.HasKey(x => x.Id);      
            builder.OwnsOne(x => x.Cnpj, c => { c.Property(y => y.Value).HasColumnName("Cnpj_Value"); });
        }
    }
}