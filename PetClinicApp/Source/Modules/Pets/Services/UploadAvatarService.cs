using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class UploadAvatarService
    {
        private readonly IPetsRepository petsRepository;
        public UploadAvatarService(IPetsRepository petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, long petId, IFormFile file)
        {
            var pet = await petsRepository.Find(petId);

            if(pet == null)
            {
                throw new AppErrorException("Pet does not exists", HttpStatusCode.NotFound);
            }

            var path = "files\\pet_avatar\\";

            //apaga o arquivo antigo
            if (string.IsNullOrEmpty(pet.Avatar))
            {
                var oldFileName = path + pet.Avatar;
                if (File.Exists(oldFileName)){
                    File.Delete(oldFileName);
                }
            }

            //sava o novo arquivo

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = file.Name.Substring(file.Name.LastIndexOf('.'));
            var newFileName = Path.GetRandomFileName();
            newFileName = newFileName + extension;


            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                await File.WriteAllBytesAsync($"{path}{newFileName}", ms.GetBuffer());
            }

            pet.Avatar = newFileName;
            await petsRepository.Update(pet);
        }
    }
}
