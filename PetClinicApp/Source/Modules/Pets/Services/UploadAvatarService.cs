using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Uploads;
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

            if(pet.UserId != loggedUserId)
            {
                throw new AppErrorException("You are not allowed to perform this action.", HttpStatusCode.Forbidden);
            }

            var newFileName = await UploadFile.Upload("pet_avatar", file, pet.Avatar);

            pet.Avatar = newFileName;
            await petsRepository.Update(pet);
        }
    }
}
