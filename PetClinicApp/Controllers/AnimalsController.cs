using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Modules.Pets.Entities;
using PetClinicApp.Source.Modules.Pets.Services;
using PetClinicApp.Source.Shared.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AnimalsController : ControllerBase
    {
        /// <summary>
        /// Retorna um animal a partir da id
        /// </summary>
        /// <param name="findAnimalService"></param>
        /// <param name="id">Id do animal</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AnimalDTO),200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDTO>> Get([FromServices] FindAnimalService findAnimalService, [FromRoute] long id)
        {
            var animal = await findAnimalService.ExecuteAsync(id);

            return Ok(animal);
        }

        /// <summary>
        /// Retorna uma lista de animais
        /// </summary>
        /// <param name="listAnimalsService"></param>
        /// <param name="search">Pesquisar</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<AnimalDTO>), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet]
        public async Task<ActionResult<List<AnimalDTO>>> Get([FromServices] ListAnimalsService listAnimalsService, [FromQuery] string search)
        {
            var animals = await listAnimalsService.ExecuteAsync(search);

            return Ok(animals);
        }

        /// <summary>
        /// Cria um novo animal
        /// </summary>
        /// <param name="createAnimalService"></param>
        /// <param name="animal">Modelo para criação de um animal</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AnimalDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPost]
        public async Task<ActionResult<AnimalDTO>> Post([FromServices] CreateAnimalService createAnimalService, [FromBody] CreateAnimalDTO animal)
        {
            var createdAnimal = await createAnimalService.ExecuteAsync(animal);

            return CreatedAtAction("Get", new { Id = createdAnimal.Id }, createdAnimal);
        }

        /// <summary>
        /// Atualiza um animal existente
        /// </summary>
        /// <param name="updateAnimalService"></param>
        /// <param name="animal">Modelo para atualizar um animal</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateAnimalService updateAnimalService, [FromBody] UpdateAnimalDTO animal)
        {
            await updateAnimalService.ExecuteAsync(animal);

            return NoContent();
        }

        /// <summary>
        /// Deleta um animal a partir da Id
        /// </summary>
        /// <param name="deleteAnimalService"></param>
        /// <param name="id">Id do animal</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AnimalDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnimalDTO>> Delete([FromServices] DeleteAnimalService deleteAnimalService, [FromRoute] long id) 
        {
            var animal = await deleteAnimalService.ExecuteAsync(id);

            return Ok(animal);
        }

    }
}
