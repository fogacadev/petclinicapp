using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Services;
using PetClinicApp.Source.Shared.Jwt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class RemindersController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderDTO>> Get([FromServices] FindReminderService findReminderService, long id)
        {
            var reminder = await findReminderService.ExecuteAsync(id);

            return Ok(reminder);
        }

        [HttpGet("pet/{petId}")]
        public async Task<ActionResult<List<ReminderDTO>>> Get([FromServices] ListRemindersService listRemindersService,[FromRoute] long petId, [FromQuery] string search)
        {
            var reminders = await listRemindersService.ExecuteAsync(petId, search);

            return Ok(reminders);
        }

        [HttpGet("user")]
        public async Task<ActionResult<List<ReminderDTO>>> Get([FromServices] ListReminderByUserService listReminderByUserService, [FromQuery] string search)
        {
            var loggedUserId = User.GetUserId();

            var reminders = await listReminderByUserService.ExecuteAsync(loggedUserId, search);

            return Ok(reminders);
        }
        [HttpPost]
        public async Task<ActionResult<ReminderDTO>> Post([FromServices] CreateReminderService createReminderService, CreateReminderDTO reminder)
        {
            var createdReminder = await createReminderService.ExecuteAsync(reminder);

            return CreatedAtAction("Get", new {Id = createdReminder.Id}, createdReminder);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateReminderService updateReminderService, [FromBody] UpdateReminderDTO reminder)
        {
            await updateReminderService.ExecuteAsync(reminder);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<ActionResult<ReminderDTO>> Put([FromServices] FinishReminderService finishReminderService, [FromRoute] long id)
        {
            var reminder = await finishReminderService.ExecuteAsync(id);

            return Ok(reminder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReminderDTO>> Delete([FromServices] DeleteReminderService deleteReminderService, [FromRoute] long id)
        {
            var reminder = await deleteReminderService.ExecuteAsync(id);

            return Ok(reminder);
        }
    }
}
