using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ActivityMapping : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
            builder.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(1050).IsRequired();
            builder.Property(x => x.Category).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
            builder.Property(x => x.City).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Venue).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Date).HasColumnType("date").IsRequired();
        }
    }
}