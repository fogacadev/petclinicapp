using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Clinics.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class ClinicServiceDbConfiguration : IEntityTypeConfiguration<ClinicService>
    {
        public void Configure(EntityTypeBuilder<ClinicService> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(255)");
        }
    }
}
