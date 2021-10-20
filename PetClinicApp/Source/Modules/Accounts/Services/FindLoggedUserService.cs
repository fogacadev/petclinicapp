using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Accounts.Entities;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Shared.Errors;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class FindLoggedUserService
    {
        private readonly AppDbContext appDbContext;
        public FindLoggedUserService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Account> ExecuteAsync(long loggedUserId)
        {

            var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == loggedUserId);

            if(user == null)
            {
                throw new AppErrorException("User does not exists.", HttpStatusCode.NotFound);
            }

            return user.ToAccount();
        }
    }
}
