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

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class MedicalHistoriesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryDTO>> Get([FromServices] FindMedicalHistoryService findMedicalHistoryService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();
            var history = await findMedicalHistoryService.ExecuteAsync(loggedUserId, id);

            return Ok(history);
        }

        [HttpGet("{petId}/pet")]
        public async Task<ActionResult<List<MedicalHistoryDTO>>> Get([FromServices] ListMedicalHistoriesService listMedicalHistoriesService, [FromRoute] long petId, [FromQuery] string search)
        {
            var loggedUserId = User.GetUserId();
            var histories = await listMedicalHistoriesService.ExecuteAsync(loggedUserId, petId, search);

            return Ok(histories);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalHistoryDTO>> Post([FromServices] CreateMedicalHistoryService createMedicalHistoryService, [FromBody] CreateMedicalHistoryDTO medicalHistory)
        {
            var loggedUserId = User.GetUserId();

            var history = await createMedicalHistoryService.ExecuteAsync(loggedUserId, medicalHistory);

            return CreatedAtAction("Get", new { Id = history.Id }, history);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateMedicalHistoryService updateMedicalHistoryService, UpdateMedicalHistoryDTO medicalHistory)
        {
            var loggedUserId = User.GetUserId();

            await updateMedicalHistoryService.ExecuteAsync(loggedUserId, medicalHistory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalHistoryDTO>> Delete([FromServices] DeleteMedicalHistoryService deleteMedicalHistoryService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            var history = await deleteMedicalHistoryService.ExecuteAsync(loggedUserId, id);

            return Ok(history);
        }

        [HttpGet("{id}/attachment")]
        public async Task<ActionResult> DownloadAttachment([FromServices] DownloadHistoryAttachmentService downloadHistoryAttachmentService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            var file = await downloadHistoryAttachmentService.ExecuteAsync(loggedUserId, id);

            

            return File(file.Buffer, file.MimeType, file.FileName);
        }

        [HttpPost("{id}/attachment")]
        public async Task<ActionResult> UploadAttachment([FromServices] UploadHistoryAttachmentService uploadHistoryAttachmentService, [FromForm] IFormFile file, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();

            await uploadHistoryAttachmentService.ExecuteAsync(loggedUserId, id, file);

            return NoContent();
        }

        [HttpDelete("{id}/attachment")]
        public async Task<ActionResult> DeleteAttachment([FromServices] DeleteHistoryAttachmentService deleteHistoryAttachmentService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();
            await deleteHistoryAttachmentService.ExecuteAsync(loggedUserId, id);

            return NoContent();
        }
    }
}
