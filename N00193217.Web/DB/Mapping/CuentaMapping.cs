using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00193217.Web.Models;

namespace N00193217.Web.DB.Mapping
{
    public class CuentaMapping : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuenta", "dbo");
            builder.HasKey(o => o.Id);
        }
    }
}
