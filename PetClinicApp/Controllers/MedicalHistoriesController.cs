using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Services;
using PetClinicApp.Source.Shared.Jwt;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PetClinicApp.Source.Shared.Errors;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class MedicalHistoriesController : ControllerBase
    {
        /// <summary>
        /// Retorna um histórico médico a partir da id
        /// </summary>
        /// <param name="findMedicalHistoryService"></param>
        /// <param name="id">Id do histórico</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MedicalHistoryDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryDTO>> Get([FromServices] FindMedicalHistoryService findMedicalHistoryService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();
            var history = await findMedicalHistoryService.ExecuteAsync(loggedUserId, id);

            return Ok(history);
        }

        /// <summary>
        /// Retorna uma lista de histórico
        /// </summary>
        /// <param name="listMedicalHistoriesService"></param>
        /// <param name="petId">Id do pet</param>
        /// <param name="search">Pesquisar</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<MedicalHistoryDTO>), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{petId}/pet")]
        public async Task<ActionResult<List<MedicalHistoryDTO>>> Get([FromServices] ListMedicalHistoriesService listMedicalHistoriesService, [FromRoute] long petId, [FromQuery] string search)
        {
            var loggedUserId = User.GetUserId();
            var histories = await listMedicalHistoriesService.ExecuteAsync(loggedUserId, petId, search);

            return Ok(histories);
        }

        /// <summary>
        /// Cria um novo histórico
        /// </summary>
        /// <param name="createMedicalHistoryService"></param>
        /// <param name="medicalHistory">Modelo para criação de um histórico</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MedicalHistoryDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPost]
        public async Task<ActionResult<MedicalHistoryDTO>> Post([FromServices] CreateMedicalHistoryService createMedicalHistoryService, [FromBody] CreateMedicalHistoryDTO medicalHistory)
        {
            var loggedUserId = User.GetUserId();

            var history = await createMedicalHistoryService.ExecuteAsync(loggedUserId, medicalHistory);

            return CreatedAtAction("Get", new { Id = history.Id }, history);
        }

        /// <summary>
        /// Atualiza um histórico existente
        /// </summary>
        /// <param name="updateMedicalHistoryService"></param>
        /// <param name="medicalHistory">Modelo para atualizar um histórico</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateMedicalHistoryService updateMedicalHistoryService, UpdateMedicalHistoryDTO medicalHistory)
        {
            var loggedUserId = User.GetUserId();

            await updateMedicalHistoryService.ExecuteAsync(loggedUserId, medicalHistory);

            return NoContent();
        }

        /// <summary>
        /// Deleta um histórico a partir da Id
        /// </summary>
        /// <param name="deleteMedicalHistoryService"></param>
        /// <param name="id">Id do histórico</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MedicalHistoryDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalHistoryDTO>> Delete([FromServices] DeleteMedicalHistoryService deleteMedicalHistoryService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            var history = await deleteMedicalHistoryService.ExecuteAsync(loggedUserId, id);

            return Ok(history);
        }

        /// <summary>
        /// Realiza o download do anexo se disponível
        /// </summary>
        /// <param name="downloadHistoryAttachmentService"></param>
        /// <param name="id">Id do histórico</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}/attachment")]
        public async Task<ActionResult> DownloadAttachment([FromServices] DownloadHistoryAttachmentService downloadHistoryAttachmentService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            var file = await downloadHistoryAttachmentService.ExecuteAsync(loggedUserId, id);

            

            return File(file.Buffer, file.MimeType, file.FileName);
        }

        /// <summary>
        /// Salva um anexo ao histórico informado
        /// </summary>
        /// <param name="uploadHistoryAttachmentService"></param>
        /// <param name="file">Arquivo do anexo</param>
        /// <param name="id">Id do histórico</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpPost("{id}/attachment")]
        public async Task<ActionResult> UploadAttachment([FromServices] UploadHistoryAttachmentService uploadHistoryAttachmentService, [FromForm] IFormFile file, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            await uploadHistoryAttachmentService.ExecuteAsync(loggedUserId, id, file);

            return NoContent();
        }

        /// <summary>
        /// Deleta um anexo a partir da Id do histórico
        /// </summary>
        /// <param name="deleteHistoryAttachmentService"></param>
        /// <param name="id">Id do histórico</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpDelete("{id}/attachment")]
        public async Task<ActionResult> DeleteAttachment([FromServices] DeleteHistoryAttachmentService deleteHistoryAttachmentService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();
            await deleteHistoryAttachmentService.ExecuteAsync(loggedUserId, id);

            return NoContent();
        }
    }
}
