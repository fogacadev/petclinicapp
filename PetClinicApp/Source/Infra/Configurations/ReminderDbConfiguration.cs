using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Reminders.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class ReminderDbConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.Property(p => p.Description)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.ReminderDate)
                .HasColumnType("date");

            builder.Property(p => p.Title)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.CreatedAt)
                .HasColumnType("date");

        }
    }
}
