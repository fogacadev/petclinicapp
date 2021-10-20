using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Services
{
    public class ListAnimalsService
    {
        private readonly IAnimalsRepository animalsRepository;
        public ListAnimalsService(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }

        public async Task<List<AnimalDTO>> ExecuteAsync(string search)
        {
            var animals = await animalsRepository.List(search);

            return animals.Select(a => a.ToDTO()).ToList();
        }
    }
}
