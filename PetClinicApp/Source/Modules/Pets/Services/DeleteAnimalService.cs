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
    public class DeleteAnimalService
    {
        private readonly IAnimalsRepository animalsRepository;
        public DeleteAnimalService(IAnimalsRepository animalsRepository)
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

            var inUse = await animalsRepository.InUse(id);
            if (inUse)
            {
                throw new AppErrorException("The animal cannot be deleted as it is being used by other records");
            }

            await animalsRepository.Delete(id);

            return animal.ToDTO();
        }
    }
}
