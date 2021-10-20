using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Pets.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Repositories.Implementations
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly AppDbContext appDbContext;
        public AnimalsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Animal> Create(Animal animal)
        {
            await appDbContext.Animals.AddAsync(animal);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(animal).State = EntityState.Detached;

            return animal;
        }

        public async Task<Animal> Delete(long id)
        {
            var animal = await Find(id);
            if(animal != null)
            {
                appDbContext.Animals.Remove(animal);
                await appDbContext.SaveChangesAsync();
            }

            return animal;
        }

        public async Task<Animal> Find(long id)
        {
            return await appDbContext
                .Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> InUse(long id)
        {
            return await appDbContext.Pets.AnyAsync(p => p.AnimalId == id);
        }

        public async Task<List<Animal>> List(string search = "")
        {
            var animals = await appDbContext
                .Animals
                .AsNoTracking()
                .ToListAsync();

            return animals.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(Animal animal)
        {
            appDbContext.Animals.Update(animal);

            await appDbContext.SaveChangesAsync();
            appDbContext.Entry(animal).State = EntityState.Detached;
        }
    }
}
