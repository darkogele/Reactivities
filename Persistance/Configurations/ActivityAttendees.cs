using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ActivityAttendees : IEntityTypeConfiguration<ActivityAttendee>
    {
        public void Configure(EntityTypeBuilder<ActivityAttendee> builder)
        {
            builder.HasKey(x => new { x.ActivityId, x.AppUserId });

            builder.HasOne(u => u.AppUser)
                .WithMany(a => a.Activities)
                .HasForeignKey(aa => aa.AppUserId)
                .IsRequired();

            builder.HasOne(a => a.Activity)
                .WithMany(u => u.Attendees)
                .HasForeignKey(aa => aa.ActivityId)
                .IsRequired();
        }
    }
}