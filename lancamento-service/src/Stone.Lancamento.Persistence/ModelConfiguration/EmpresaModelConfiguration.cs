using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.Lancamento.Domain.Contas.Entities;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    public class EmpresaModelConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Cnpj, c => { c.Property(y => y.Value).HasColumnName("Cnpj_Value"); });
        }
    }
}