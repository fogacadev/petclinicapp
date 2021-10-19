using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class DownloadAvatarService
    {
        private readonly IPetsRepository petsRepository;
        public DownloadAvatarService(IPetsRepository petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public async Task<FileContentResult> ExecuteAsync(long loggedUserId, long id)
        {
            var pet = await petsRepository.Find(id);

            if (pet == null)
            {
                throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
            }

            var path = AppDomain.CurrentDomain.BaseDirectory + "\\files\\account_avatar\\";
            var filename = $"{path}{pet.Avatar}";

            //apaga o anexo antigo
            if (!string.IsNullOrEmpty(pet.Avatar))
            {
                if (File.Exists(filename))
                {
                    var extension = pet.Avatar.Substring(pet.Avatar.LastIndexOf('.'));
                    extension = extension.Remove(0, 1);
                    var mimeType = MimeTypeMap.GetMimeType(extension);

                    var bytes = await File.ReadAllBytesAsync(filename);
                    return new FileContentResult(bytes, mimeType);
                }
            }

            throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
        }
    }
}
