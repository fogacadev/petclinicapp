using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Clinics.DTO;
using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ClinicsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDTO>> Get([FromServices] FindClinicService findClinicService, [FromRoute]long id)
        {
            var clinic = await findClinicService.ExecuteAsync(id);
            return Ok(clinic);
        }

        [HttpGet]
        public async Task<ActionResult<List<ClinicDTO>>> Get([FromServices] ListClinicsService listClinicsService, [FromQuery] string search)
        {
            var clinics = await listClinicsService.ExecuteAsync(search);

            return Ok(clinics);
        }

        [HttpPost]
        public async Task<ActionResult<ClinicDTO>> Post([FromServices] CreateClinicService createClinicService, [FromBody] CreateClinicDTO clinic)
        {
            var createdClinic = await createClinicService.ExecuteAsync(clinic);

            return CreatedAtAction("Get", new { Id = createdClinic.Id }, createdClinic);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateClinicService updateClinicService, [FromBody] UpdateClinicDTO clinic)
        {
            await updateClinicService.ExecuteAsync(clinic);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClinicDTO>> Delete([FromServices] DeleteClinicService deleteClinicService, [FromRoute] long id)
        {
            var clinic = await deleteClinicService.ExecuteAsync(id);

            return Ok(clinic);
        }

        [HttpPost("{id}/avatar")]
        public async Task<ActionResult> UploadAvatar([FromServices] UploadClinicAvatarService uploadClinicAvatarService, [FromRoute] long id, [FromForm] IFormFile file)
        {
            await uploadClinicAvatarService.ExecuteAsync(id, file);

            return NoContent();
        }

        [HttpGet("{id}/avatar")]
        public async Task<ActionResult> DownloadAvatar([FromServices] DownloadClinicAvatarService downloadClinicAvatarService, [FromRoute] long id)
        {
            var file = await downloadClinicAvatarService.ExecuteAsync(id);

            return File(file.Buffer, file.MimeType);
        }
    }
}
