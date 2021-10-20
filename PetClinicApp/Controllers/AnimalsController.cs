using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AnimalsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDTO>> Get([FromServices] FindAnimalService findAnimalService, [FromRoute] long id)
        {
            var animal = await findAnimalService.ExecuteAsync(id);

            return Ok(animal);
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimalDTO>>> Get([FromServices] ListAnimalsService listAnimalsService, [FromQuery] string search)
        {
            var animals = await listAnimalsService.ExecuteAsync(search);

            return Ok(animals);
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDTO>> Post([FromServices] CreateAnimalService createAnimalService, [FromBody] CreateAnimalDTO animal)
        {
            var createdAnimal = await createAnimalService.ExecuteAsync(animal);

            return CreatedAtAction("Get", new { Id = createdAnimal.Id }, createdAnimal);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateAnimalService updateAnimalService, [FromBody] UpdateAnimalDTO animal)
        {
            await updateAnimalService.ExecuteAsync(animal);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AnimalDTO>> Delete([FromServices] DeleteAnimalService deleteAnimalService, [FromRoute] long id) 
        {
            var animal = await deleteAnimalService.ExecuteAsync(id);

            return Ok(animal);
        }

    }
}
