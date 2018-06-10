using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stone.Lancamento.Domain.Lancamentos.Entities;

namespace Stone.Lancamento.Persistence.ModelConfiguration
{
    public class PagamentoModelConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasOne(x => x.ContaBancaria);
        }
    }
}