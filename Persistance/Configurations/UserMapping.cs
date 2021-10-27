using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
            builder.Property(x => x.Bio).HasColumnType("nvarchar").HasMaxLength(2000);
            builder.Property(x => x.DisplayName).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();

            builder.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User).HasForeignKey(u => u.UserId).IsRequired();
        }
    }
}