using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00193217.Web.Models;

namespace N00193217.Web.DB.Mapping
{
    public class TransaccionMapping : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
            builder.ToTable("Transaccion", "dbo");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Cuenta)
                .WithMany()
                .HasForeignKey(o => o.IdCuenta);
        }
    }
}
