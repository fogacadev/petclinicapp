using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Pets.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class AnimalDbConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("varchar(255)");
        }
    }
}
