using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimalsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> Get([FromServices] FindAnimalService findAnimalService, [FromRoute] long id)
        {
            var animal = await findAnimalService.ExecuteAsync(id);

            return Ok(animal);
        }

        [HttpGet]
        public async Task<ActionResult<List<Animal>>> Get([FromServices] ListAnimalsService listAnimalsService, [FromQuery] string search)
        {
            var animals = await listAnimalsService.ExecuteAsync(search);

            return Ok(animals);
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> Post([FromServices] CreateAnimalService createAnimalService, [FromBody] AnimalDTO animal)
        {
            var createdAnimal = await createAnimalService.ExecuteAsync(animal);

            return CreatedAtAction("Get", new { Id = createdAnimal.Id }, createdAnimal);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateAnimalService updateAnimalService, [FromBody] AnimalDTO animal)
        {
            await updateAnimalService.ExecuteAsync(animal);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromServices] DeleteAnimalService deleteAnimalService, [FromRoute] long id) 
        {
            var animal = await deleteAnimalService.ExecuteAsync(id);

            return Ok(animal);
        }

    }
}
