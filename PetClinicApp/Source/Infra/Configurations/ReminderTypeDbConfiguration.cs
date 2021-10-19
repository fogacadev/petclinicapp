using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Reminders.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class ReminderTypeDbConfiguration : IEntityTypeConfiguration<ReminderType>
    {
        public void Configure(EntityTypeBuilder<ReminderType> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(255)");
        }
    }
}
