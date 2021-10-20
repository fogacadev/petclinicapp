using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
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

        public async Task<FileModel> ExecuteAsync(long loggedUserId, long id)
        {
            var pet = await petsRepository.Find(id);

            if (pet == null)
            {
                throw new AppErrorException("Avatar does not exists", HttpStatusCode.NotFound);
            }

            var file = await UploadFile.Download("pet_avatar", pet.Avatar);

            if(file == null)
            {
                throw new AppErrorException("Avatar does not exists.", HttpStatusCode.NotFound);
            }
            return file;
        }
    }
}
