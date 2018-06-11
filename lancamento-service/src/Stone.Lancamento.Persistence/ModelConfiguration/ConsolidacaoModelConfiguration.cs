using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.Lancamento.Domain.Lancamentos.Entities;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    public class ConsolidacaoModelConfiguration : IEntityTypeConfiguration<Consolidacao>
    {
        public void Configure(EntityTypeBuilder<Consolidacao> builder)
        {
            builder.HasMany(x => x.Pagamentos);
            builder.HasMany(x => x.Recebimentos);
        }
    }
}