using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ClinicsController : ControllerBase
    {

        /// <summary>
        /// Retorna uma clinica a partir do Id
        /// </summary>
        /// <param name="findClinicService"></param>
        /// <param name="id">Id da clinica</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClinicDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDTO>> Get([FromServices] FindClinicService findClinicService, [FromRoute]long id)
        {
            var clinic = await findClinicService.ExecuteAsync(id);
            return Ok(clinic);
        }

        /// <summary>
        /// Retorna uma lista de clinicas
        /// </summary>
        /// <param name="listClinicsService"></param>
        /// <param name="search">Pesquisar</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ClinicDTO>), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet]
        public async Task<ActionResult<List<ClinicDTO>>> Get([FromServices] ListClinicsService listClinicsService, [FromQuery] string search)
        {
            var clinics = await listClinicsService.ExecuteAsync(search);

            return Ok(clinics);
        }


        /// <summary>
        /// Cria uma nova clinica
        /// </summary>
        /// <param name="createClinicService"></param>
        /// <param name="clinic">Modelo para criação de uma clinica</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClinicDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPost]
        public async Task<ActionResult<ClinicDTO>> Post([FromServices] CreateClinicService createClinicService, [FromBody] CreateClinicDTO clinic)
        {
            var createdClinic = await createClinicService.ExecuteAsync(clinic);

            return CreatedAtAction("Get", new { Id = createdClinic.Id }, createdClinic);
        }


        /// <summary>
        /// Atualiza uma clinica existente
        /// </summary>
        /// <param name="updateClinicService"></param>
        /// <param name="clinic">Modelo para atualizar uma clinica</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateClinicService updateClinicService, [FromBody] UpdateClinicDTO clinic)
        {
            await updateClinicService.ExecuteAsync(clinic);

            return NoContent();
        }

        /// <summary>
        /// Deleta uma clinica a partir da Id
        /// </summary>
        /// <param name="deleteClinicService"></param>
        /// <param name="id">Id da clinica</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClinicDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClinicDTO>> Delete([FromServices] DeleteClinicService deleteClinicService, [FromRoute] long id)
        {
            var clinic = await deleteClinicService.ExecuteAsync(id);

            return Ok(clinic);
        }

        /// <summary>
        /// Atualizar o avatar da clinica
        /// </summary>
        /// <param name="uploadClinicAvatarService"></param>
        /// <param name="file">Arquivo contendo o avatar da clinica. .png .jpg .jpeg</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPost("{id}/avatar")]
        public async Task<ActionResult> UploadAvatar([FromServices] UploadClinicAvatarService uploadClinicAvatarService, [FromRoute] long id, [FromForm] IFormFile file)
        {
            await uploadClinicAvatarService.ExecuteAsync(id, file);

            return NoContent();
        }

        /// <summary>
        /// Retorna o avatar da clinica
        /// </summary>
        /// <param name="downloadClinicAvatarService"></param>
        /// <param name="id">Id da clinica</param>
        /// <returns>Imagem da clinica</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}/avatar")]
        public async Task<ActionResult> DownloadAvatar([FromServices] DownloadClinicAvatarService downloadClinicAvatarService, [FromRoute] long id)
        {
            var file = await downloadClinicAvatarService.ExecuteAsync(id);

            return File(file.Buffer, file.MimeType);
        }
    }
}
