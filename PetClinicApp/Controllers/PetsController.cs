using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Pets.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using PetClinicApp.Source.Modules.Pets.Services;
using PetClinicApp.Source.Modules.Pets.DTO;
using PetClinicApp.Source.Shared.Jwt;
using Microsoft.AspNetCore.Http;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<List<PetDTO>>> Get([FromServices] ListPetsService listPetsService, [FromQuery] string search)
        {
            long loggedUserId = User.GetUserId();

            var pets = await listPetsService.ExecuteAsync(loggedUserId, search);

            return Ok(pets);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> Get([FromServices] FindPetService findPetService, [FromRoute] long id)
        {
            long loggedUserId = User.GetUserId();

            //precisa colocar o id do usuário pra n permitir trazer pet de outro dono
            var pet = await findPetService.ExcuteAsync(loggedUserId, id);

            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromServices] CreatePetService createPetService, [FromBody] PetDTO pet)
        {
            var loggedUserId = User.GetUserId();

            var createdPet = await createPetService.ExecuteAsync(pet);

            return Ok(createdPet);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromServices] UpdatePetService updatePetService, [FromBody] PetDTO pet)
        {
            var loggedUserId = User.GetUserId();

            await updatePetService.ExecuteAsync(pet);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PetDTO>> Delete([FromServices] DeletePetService deletePetService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            var pet = await deletePetService.ExecuteAsync(id);

            return Ok(pet);
        }

        [HttpPost("{id}/avatar")]
        public async Task<ActionResult> UploadAvatar([FromServices] UploadAvatarService uploadAvatarService, [FromRoute] long id, [FromForm] IFormFile file)
        {
            var loggedUserId = User.GetUserId();

            await uploadAvatarService.ExecuteAsync(loggedUserId, id, file);

            return NoContent();
        }

        [HttpGet("{id}/avatar")]
        public async Task<ActionResult> DonwloadAvatar([FromServices] DownloadAvatarService downloadAvatarService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            var fileContentResult = await downloadAvatarService.ExecuteAsync(loggedUserId, id);

            return fileContentResult;
        }
    }
}
