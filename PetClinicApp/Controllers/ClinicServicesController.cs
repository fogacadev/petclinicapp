using Microsoft.AspNetCore.Authorization;
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
    public class ClinicServicesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicServiceDTO>> Get([FromServices] FindClinicServicesService findClinicServicesService, [FromRoute] long id)
        {
            var service = await findClinicServicesService.ExecuteAsync(id);

            return Ok(service);
        }

        [HttpGet("{clinicId}/clinic")]
        public async Task<ActionResult<List<ClinicServiceDTO>>> Get([FromServices] ListClinicServicesUseCase listClinicServicesUseCase, [FromRoute] long clinicId, [FromQuery] string search)
        {
            var services = await listClinicServicesUseCase.Execute(clinicId, search);

            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult<ClinicServiceDTO>> Post([FromServices] CreateClinicServiceSerivice createClinicServiceSerivice, [FromBody] CreateClinicServiceDTO clinicService)
        {
            var createdClinicService = await createClinicServiceSerivice.ExecuteAsync(clinicService);

            return CreatedAtAction("Get", new { Id = createdClinicService.Id }, createdClinicService);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateClinicServiceService updateClinicServiceService, [FromBody] UpdateClinicServiceDTO clinicService)
        {
            await updateClinicServiceService.ExecuteAsync(clinicService);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClinicServiceDTO>> Delete([FromServices] DeleteClinicServiceService deleteClinicServiceService, [FromRoute] long id)
        {
            var service = await deleteClinicServiceService.ExecuteAsync(id);

            return Ok(service);
        }
    }
}
