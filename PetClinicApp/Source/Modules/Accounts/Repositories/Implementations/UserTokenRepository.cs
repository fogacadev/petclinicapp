using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Accounts.Entities;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Repositories.Implementations
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly AppDbContext appDbContext;
        public UserTokenRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<UserToken> Create(UserToken userToken)
        {
            await appDbContext.UserTokens.AddAsync(userToken);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(userToken).State = EntityState.Detached;

            return userToken;
        }

        public async Task Delete(long id)
        {
            var userToken = await appDbContext.UserTokens.FirstOrDefaultAsync(t => t.Id == id);
            if(userToken != null)
            {
                appDbContext.UserTokens.Remove(userToken);
                await appDbContext.SaveChangesAsync();
            }
        }

        public Task<UserToken> Find(long id)
        {
            return appDbContext.UserTokens.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<UserToken> FindByRefreshToken(string refreshToken)
        {
            return appDbContext.UserTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
        }
    }
}
