using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Clinics.DTO;
using PetClinicApp.Source.Modules.Clinics.Entities;
using PetClinicApp.Source.Modules.Clinics.Services;
using PetClinicApp.Source.Shared.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ClinicServicesController : ControllerBase
    {
        /// <summary>
        /// Retorna um serviço da clinica
        /// </summary>
        /// <param name="findClinicServicesService"></param>
        /// <param name="id">Id do serviço</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClinicServiceDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicServiceDTO>> Get([FromServices] FindClinicServicesService findClinicServicesService, [FromRoute] long id)
        {
            var service = await findClinicServicesService.ExecuteAsync(id);

            return Ok(service);
        }

        /// <summary>
        /// Retorna uma lista de serviços
        /// </summary>
        /// <param name="listClinicServicesUseCase"></param>
        /// <param name="clinicId">id da clinica</param>
        /// <param name="search">Pesquisar</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ClinicServiceDTO>), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{clinicId}/clinic")]
        public async Task<ActionResult<List<ClinicServiceDTO>>> Get([FromServices] ListClinicServicesUseCase listClinicServicesUseCase, [FromRoute] long clinicId, [FromQuery] string search)
        {
            var services = await listClinicServicesUseCase.Execute(clinicId, search);

            return Ok(services);
        }

        /// <summary>
        /// Cria um novo serviço
        /// </summary>
        /// <param name="createClinicServiceSerivice"></param>
        /// <param name="clinicService">Modelo para criação de um serviço</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClinicServiceDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPost]
        public async Task<ActionResult<ClinicServiceDTO>> Post([FromServices] CreateClinicServiceSerivice createClinicServiceSerivice, [FromBody] CreateClinicServiceDTO clinicService)
        {
            var createdClinicService = await createClinicServiceSerivice.ExecuteAsync(clinicService);

            return CreatedAtAction("Get", new { Id = createdClinicService.Id }, createdClinicService);
        }

        /// <summary>
        /// Atualiza um serviço existente
        /// </summary>
        /// <param name="updateClinicServiceService"></param>
        /// <param name="clinicService">Modelo para atualizar um serviço</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateClinicServiceService updateClinicServiceService, [FromBody] UpdateClinicServiceDTO clinicService)
        {
            await updateClinicServiceService.ExecuteAsync(clinicService);

            return NoContent();
        }

        /// <summary>
        /// Deleta um serviço a partir da Id
        /// </summary>
        /// <param name="deleteClinicServiceService"></param>
        /// <param name="id">Id do serviço</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClinicServiceDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClinicServiceDTO>> Delete([FromServices] DeleteClinicServiceService deleteClinicServiceService, [FromRoute] long id)
        {
            var service = await deleteClinicServiceService.ExecuteAsync(id);

            return Ok(service);
        }
    }
}
