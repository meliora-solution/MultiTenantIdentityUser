using IdentityUser100.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityUser100.Configuration
{
    public class PropertyConfig : IEntityTypeConfiguration<Property>
    {


        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(b => b.PropertyId);
            builder.Property(b => b.PropertyId).IsRequired(true).HasColumnType("varchar(100)");
            builder.HasIndex(b => b.PropertyName).IsUnique(true);
            builder.Property(b => b.PropertyName).IsRequired(true).HasColumnType("varchar(50)");
        }
    }
}
