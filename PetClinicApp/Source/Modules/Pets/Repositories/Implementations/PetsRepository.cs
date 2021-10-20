using Microsoft.EntityFrameworkCore;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Pets.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Pets.Repositories.Implementations
{
    public class PetsRepository : IPetsRepository
    {
        private readonly AppDbContext appDbContext;
        public PetsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Pet> Create(Pet pet)
        {
            await appDbContext.Pets.AddAsync(pet);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(pet).State = EntityState.Detached;

            return pet;
        }

        public async Task<Pet> Delete(long id)
        {
            var pet = await Find(id);

            if(pet != null)
            {
                appDbContext.Pets.Remove(pet);
                await appDbContext.SaveChangesAsync();
            }

            return pet;
        }

        public Task<Pet> Find(long id)
        {
            return appDbContext.Pets.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pet>> List(long userId, string search = "")
        {
            var pets = await appDbContext
                .Pets
                .Where(p => p.UserId == userId)
                .AsNoTracking()
                .ToListAsync();


            return pets.AsQueryable().Filter(search).ToList();
        }

        public async Task Update(Pet pet)
        {
            appDbContext.Pets.Update(pet);
            await appDbContext.SaveChangesAsync();

            appDbContext.Entry(pet).State = EntityState.Detached;
        }
    }
}
