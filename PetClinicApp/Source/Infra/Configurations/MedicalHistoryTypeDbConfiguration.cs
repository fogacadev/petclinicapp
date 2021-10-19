using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class MedicalHistoryTypeDbConfiguration : IEntityTypeConfiguration<MedicalHistoryType>
    {
        public void Configure(EntityTypeBuilder<MedicalHistoryType> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(255)");
        }
    }
}
