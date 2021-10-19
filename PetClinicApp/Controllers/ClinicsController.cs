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
    public class ClinicsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinic>> Get([FromServices] FindClinicService findClinicService, [FromRoute]long id)
        {
            var clinic = await findClinicService.ExecuteAsync(id);
            return Ok(clinic);
        }

        [HttpGet]
        public async Task<ActionResult<List<Clinic>>> Get([FromServices] ListClinicsService listClinicsService, [FromQuery] string search)
        {
            var clinics = await listClinicsService.ExecuteAsync(search);

            return Ok(clinics);
        }

        [HttpPost]
        public async Task<ActionResult<Clinic>> Post([FromServices] CreateClinicService createClinicService, [FromBody] ClinicDTO clinic)
        {
            var createdClinic = await createClinicService.ExecuteAsync(clinic);

            return CreatedAtAction("Get", new { Id = createdClinic.Id }, createdClinic);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateClinicService updateClinicService, [FromBody] ClinicDTO clinic)
        {
            await updateClinicService.ExecuteAsync(clinic);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Clinic>> Delete([FromServices] DeleteClinicService deleteClinicService, [FromRoute] long id)
        {
            var clinic = await deleteClinicService.ExecuteAsync(id);

            return Ok(clinic);
        }
    }
}
