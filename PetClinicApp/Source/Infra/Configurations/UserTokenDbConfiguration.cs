using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinicApp.Source.Modules.Accounts.Entities;

namespace PetClinicApp.Source.Infra.Configurations
{
    public class UserTokenDbConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.Property(p => p.Token)
                .HasColumnType("text");

            builder.Property(p => p.ExpiresOn)
                .HasColumnType("datetime");
        }
    }
}
