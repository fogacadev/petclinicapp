using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class FindPetService : ServiceBase
    {
        private readonly IPetsRepository petsRepository;
        public FindPetService(IPetsRepository petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        public async Task<Pet> ExcuteAsync(long userId, long id)
        {
            var pet = await petsRepository.Find(id);

            if(pet == null)
            {
                throw new AppErrorException("Pet does not exists.", HttpStatusCode.NotFound);
            }

            if(pet.UserId != userId)
            {
                throw new AppErrorException("Pet does not exists.");
            }

            return pet;
        }
    }
}
