using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Accounts.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class UserDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FullName)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Email)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Avatar)
                .HasColumnType("varchar(255)");

            builder.Property(p => p.BirthDate)
                .HasColumnType("date");

            builder.Property(p => p.CreatedAt)
                .HasColumnType("date");

            builder.Property(p => p.PasswordHash)
                .HasColumnType("text");

            builder.Property(p => p.Avatar)
                .HasColumnType("text");
        }
    }
}
