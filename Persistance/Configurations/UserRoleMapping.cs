using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserRoleMapping : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.ToTable("UserRoles");

            // Primary key
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(u => u.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(r => r.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(r => r.RoleId);
        }
    }
}