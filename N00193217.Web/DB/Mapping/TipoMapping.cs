using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00193217.Web.Models;

namespace N00193217.Web.DB.Mapping
{
    public class TipoMapping : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.ToTable("Tipo", "dbo");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Categoria)
                .WithMany()
                .HasForeignKey(o => o.IdCategoria);
        }
    }
}
