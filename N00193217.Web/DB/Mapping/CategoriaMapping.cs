using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00193217.Web.Models;

namespace N00193217.Web.DB.Mapping
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria", "dbo");
            builder.HasKey(o => o.Id);
        }
    }
}
