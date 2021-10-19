using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra.Configurations;
using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Reminders.Entities;

namespace PetClinicApp.Source.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<ClinicService> ClinicServices { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderType> ReminderTypes { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<MedicalHistoryType> MedicalHistoryTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AnimalDbConfiguration());
            builder.ApplyConfiguration(new ClinicDbConfiguration());
            builder.ApplyConfiguration(new ClinicServiceDbConfiguration());
            builder.ApplyConfiguration(new MedicalHistoryDbConfiguration());
            builder.ApplyConfiguration(new MedicalHistoryTypeDbConfiguration());
            builder.ApplyConfiguration(new PetDbConfiguration());
            builder.ApplyConfiguration(new ReminderDbConfiguration());
            builder.ApplyConfiguration(new ReminderTypeDbConfiguration());
            builder.ApplyConfiguration(new UserDbConfiguration());
            builder.ApplyConfiguration(new UserTokenDbConfiguration());
        }
    }
}
