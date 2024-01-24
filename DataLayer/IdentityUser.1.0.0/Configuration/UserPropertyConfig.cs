using IdentityUser100.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityUser100.Configuration
{
    public class UserPropertyConfig : IEntityTypeConfiguration<UserProperty>
    {
        public void Configure(EntityTypeBuilder<UserProperty> builder)
        {
            builder.HasKey(b => new { b.UserId, b.PropertyId });
            builder.Property(b => b.UserId).IsRequired(true).HasColumnType("varchar(256)");
            builder.Property(b => b.PropertyId).IsRequired(true).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(b => b.PropertyValue).IsRequired(true).HasColumnType("varchar").HasMaxLength(256);
            builder
               .HasOne(b => b.property)
               .WithMany(b => b.UserProperties).HasForeignKey(b => b.PropertyId);
            builder
                .HasOne(b => b.ApplicationUser)
                .WithMany(b => b.UserProperties).HasForeignKey(b => b.UserId);
        }
    }
}
