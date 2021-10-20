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
    public class UpdatePetService : ServiceBase
    {
        private readonly IPetsRepository petsRepository;
        private readonly IAnimalsRepository animalsRepository;
        private readonly IUsersRepository usersRepository;

        public UpdatePetService(IPetsRepository petsRepository,
            IAnimalsRepository animalsRepository,
            IUsersRepository usersRepository)
        {
            this.petsRepository = petsRepository;
            this.animalsRepository = animalsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task ExecuteAsync(long loggedUserId, UpdatePetDTO pet)
        {
            ValidateModel(pet);

            var animalExists = await animalsRepository.Find(pet.AnimalId);

            if (animalExists == null)
            {
                throw new AppErrorException("Animal does not exists", HttpStatusCode.NotFound);
            }

            var userExists = await usersRepository.Find(loggedUserId);
            if (userExists == null)
            {
                throw new AppErrorException("User does not exists", HttpStatusCode.NotFound);
            }

            var createdPet = await petsRepository.Find(pet.Id);

            if(createdPet == null)
            {
                throw new AppErrorException("Pet does not exists.", HttpStatusCode.NotFound);
            }

            if(createdPet.UserId != loggedUserId)
            {
                throw new AppErrorException("You do not have permission to perform this action.", HttpStatusCode.Forbidden);
            }

            createdPet.Name = pet.Name;
            createdPet.AnimalId = pet.AnimalId;
            createdPet.BornIn = pet.BornIn;


            await petsRepository.Update(createdPet);
        }
    }
}
