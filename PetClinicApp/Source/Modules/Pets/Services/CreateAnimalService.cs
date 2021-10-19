using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class CreateAnimalService : ServiceBase
    {
        private readonly IAnimalsRepository animalsRepository;
        public CreateAnimalService(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }

        public async Task<Animal> ExecuteAsync(AnimalDTO animal)
        {
            ValidateModel(animal);

            var createdAnimal = await animalsRepository.Create(animal.ToEntity());

            return createdAnimal;
        }
    }
}
