using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Accounts.Services
{
    public class DownloadAccountAvatarService
    {
        private readonly IUsersRepository usersRepository;
        public DownloadAccountAvatarService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }


        public async Task<FileModel> ExecuteAsync(long loggedUserId)
        {
            var user = await usersRepository.Find(loggedUserId);

            if (user == null)
            {
                throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
            }

            var file = await UploadFile.Download("account_avatar", user.Avatar);

            if(file == null)
            {
                throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
            }

            return file;

            
        }
    }
}
