using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class FindAnimalService
    {
        private readonly IAnimalsRepository animalsRepository;
        public FindAnimalService(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }

        public async Task<AnimalDTO> ExecuteAsync(long id)
        {
            var animal = await animalsRepository.Find(id);

            if(animal == null)
            {
                throw new AppErrorException("Animal does not exists", HttpStatusCode.NotFound);
            }

            return animal.ToDTO();
        }
    }
}
