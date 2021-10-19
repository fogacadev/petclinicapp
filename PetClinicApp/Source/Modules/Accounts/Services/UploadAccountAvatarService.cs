using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Shared.Errors;
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

            var path = AppDomain.CurrentDomain.BaseDirectory + "\\files\\account_avatar\\";

            //apaga o arquivo antigo
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                var oldFileName = path + user.Avatar;
                if (File.Exists(oldFileName))
                {
                    File.Delete(oldFileName);
                }
            }

            //sava o novo arquivo

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var newFileName = Path.GetRandomFileName();
            newFileName = newFileName + extension;


            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                await File.WriteAllBytesAsync($"{path}{newFileName}", ms.GetBuffer());
            }

            user.Avatar = newFileName;
            await usersRepository.Update(user);
        }
    }
}
