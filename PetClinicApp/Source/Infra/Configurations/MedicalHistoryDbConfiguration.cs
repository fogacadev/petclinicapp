using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class MedicalHistoryDbConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.Property(p => p.Title)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Description)
                .HasColumnType("text");

            builder.Property(p => p.Attachment)
                .HasColumnType("text");
        }
    }
}
