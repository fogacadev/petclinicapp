using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Accounts.Models;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext appDbContext;
        public UsersRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<User> Create(User user)
        {
            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(user).State = EntityState.Detached;

            return user;
        }

        public async Task<User> Find(long id)
        {
            return await appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> Register(User user)
        {
            await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(user).State = EntityState.Detached;

            return user;
        }

        public async Task Update(User user)
        {
            appDbContext.Users.Update(user);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(user).State = EntityState.Detached;
        }
    }
}
