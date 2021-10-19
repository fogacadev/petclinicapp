using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Pets.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class PetDbConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.Property(p => p.Avatar)
                .HasColumnType("text");

            builder.Property(p => p.Name)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.BornIn)
                .HasColumnType("date");

            builder.Property(p => p.CreatedAt)
                .HasColumnType("date");
        }
    }
}
