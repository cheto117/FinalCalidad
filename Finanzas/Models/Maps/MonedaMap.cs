using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finanzas.Models.Maps
{
    public class MonedaMap : IEntityTypeConfiguration<Moneda>
    {
        public void Configure(EntityTypeBuilder<Moneda> builder)
        {
            builder.ToTable("Moneda");
            builder.HasKey(o => o.Id);
        }
    }
}
