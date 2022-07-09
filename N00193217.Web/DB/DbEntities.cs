using Microsoft.EntityFrameworkCore;
using N00193217.Web.Models;
using N00193217.Web.DB.Mapping;

namespace N00193217.Web.DB
{
    public class DbEntities : DbContext
    {
        public virtual DbSet<Categoria> categorias { get; set; }
        public virtual DbSet<Cuenta> cuentas { get; set; }
        public virtual DbSet<Tipo> tipos { get; set; }
        public virtual DbSet<Transaccion> transaccions { get; set; }
        public DbEntities() { }
        public DbEntities(DbContextOptions<DbEntities> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new CuentaMapping());
            modelBuilder.ApplyConfiguration(new TipoMapping());
            modelBuilder.ApplyConfiguration(new TransaccionMapping());
        }
    }
}
