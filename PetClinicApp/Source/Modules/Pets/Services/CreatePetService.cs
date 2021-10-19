using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Modules.Pets.DTO;
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
    public class CreatePetService : ServiceBase
    {
        private readonly IPetsRepository petsRepository;
        private readonly IAnimalsRepository animalsRepository;
        private readonly IUsersRepository usersRepository;

        public CreatePetService(IPetsRepository petsRepository, 
            IAnimalsRepository animalsRepository,
            IUsersRepository usersRepository)
        {
            this.petsRepository = petsRepository;
            this.animalsRepository = animalsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<Pet> ExecuteAsync(PetDTO pet)
        {
            ValidateModel(pet);

            var animalExists = await animalsRepository.Find(pet.AnimalId);

            if(animalExists == null)
            {
                throw new AppErrorException("Animal does not exists", HttpStatusCode.NotFound);
            }

            var userExists = await usersRepository.Find(pet.UserId);
            if(userExists == null)
            {
                throw new AppErrorException("User does not exists", HttpStatusCode.NotFound);
            }

            var createPet = pet.ToEntity();
            createPet.CreatedAt = DateTime.Now;

            var createdPet = await petsRepository.Create(createPet);


            return createdPet;
        }
    }
}
