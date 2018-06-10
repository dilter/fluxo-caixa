using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.Lancamento.Domain.Contas.Entities;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    public class ContaBancariaModelConfiguration : IEntityTypeConfiguration<ContaBancaria>
    {
        public void Configure(EntityTypeBuilder<ContaBancaria> builder)
        {
            builder.HasOne(x => x.Empresa);
        }
    }
}