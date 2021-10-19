using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Clinics.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class ClinicDbConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Descripton)
                .HasColumnType("text");

            builder.Property(p => p.Avatar)
                .HasColumnType("text");

        }
    }
}
