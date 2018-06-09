using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.Lancamento.Domain.Contas.Entities;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    public class EmpresaModelConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasMany(x => x.ContasBancarias).WithOne(x => x.Empresa);
        }
    }
}