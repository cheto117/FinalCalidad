using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finanzas.Models.Maps
{
    public class CuentaMap : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuenta");
            builder.HasKey(o => o.Id);
            
            builder.HasOne(o => o.Categoria).
                WithMany().
                HasForeignKey(o => o.IdCategoria);

            builder.HasOne(o => o.Moneda).
                WithMany().
                HasForeignKey(o => o.IdMoneda);

            builder.HasMany(o => o.Transaccions).
                WithOne().
                HasForeignKey(o => o.IdCuenta);
        }
    }
}
