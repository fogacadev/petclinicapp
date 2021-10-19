using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class UpdateAnimalService
    {
        private readonly IAnimalsRepository animalsRepository;
        public UpdateAnimalService(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }

        public async Task ExecuteAsync(AnimalDTO animal)
        {
            var createdAnimal = await animalsRepository.Find(animal.Id);

            if(createdAnimal == null)
            {
                throw new AppErrorException("Animal does not exists");
            }

            var updateAnimal = animal.ToEntity();
            updateAnimal.CreatedAt = createdAnimal.CreatedAt;

            await animalsRepository.Update(updateAnimal);
        }
    }
}
