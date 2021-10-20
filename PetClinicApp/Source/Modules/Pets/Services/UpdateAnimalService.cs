using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class UpdateAnimalService : ServiceBase
    {
        private readonly IAnimalsRepository animalsRepository;
        public UpdateAnimalService(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }

        public async Task ExecuteAsync(UpdateAnimalDTO animal)
        {

            ValidateModel(animal);

            var createdAnimal = await animalsRepository.Find(animal.Id);

            if(createdAnimal == null)
            {
                throw new AppErrorException("Animal does not exists");
            }
            createdAnimal.Name = animal.Name;

            await animalsRepository.Update(createdAnimal);
        }
    }
}
