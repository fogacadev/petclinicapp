using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class UploadAccountAvatarService
    {
        private readonly IUsersRepository usersRepository;
        public UploadAccountAvatarService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, IFormFile file)
        {
            var user = await usersRepository.Find(loggedUserId);

            if (user == null)
            {
                throw new AppErrorException("Account does not exists", HttpStatusCode.NotFound);
            }


            var newFileName = await UploadFile.Upload("account_avatar", file);

            user.Avatar = newFileName;
            await usersRepository.Update(user);
        }
    }
}
