using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.Lancamento.Domain.Lancamentos.Entities;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    public class RecebimentoModelConfiguration : IEntityTypeConfiguration<Recebimento>
    {
        public void Configure(EntityTypeBuilder<Recebimento> builder)
        {
            builder.HasOne(x => x.ContaBancaria);
        }
    }
}