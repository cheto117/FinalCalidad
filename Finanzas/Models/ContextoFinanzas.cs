using Finanzas.Models.Maps;
using Microsoft.EntityFrameworkCore;

namespace Finanzas.Models
{
    public class ContextoFinanzas : DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Transaccion> Transaccions { get; set; }
        public DbSet<Moneda> Monedas { get; set; }

        public ContextoFinanzas(DbContextOptions<ContextoFinanzas> o) : base(o) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CuentaMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
            modelBuilder.ApplyConfiguration(new TransaccionMap());
            modelBuilder.ApplyConfiguration(new MonedaMap());
        }
    }
}
