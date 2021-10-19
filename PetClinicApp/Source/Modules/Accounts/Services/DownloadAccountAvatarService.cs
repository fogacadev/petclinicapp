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

            var path = AppDomain.CurrentDomain.BaseDirectory + "\\files\\account_avatar\\";
            var filename = $"{path}{user.Avatar}";

            //apaga o anexo antigo
            if (!string.IsNullOrEmpty(user.Avatar))
            {
                if (File.Exists(filename))
                {
                    var extension = user.Avatar.Substring(user.Avatar.LastIndexOf('.'));
                    extension = extension.Remove(0, 1);
                    var mimeType = MimeTypeMap.GetMimeType(extension);

                    var bytes = await File.ReadAllBytesAsync(filename);
                    return new FileModel(bytes, mimeType);
                }
            }

            throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
        }
    }
}
