using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Reminders.DTO;
using PetClinicApp.Source.Modules.Reminders.Entities;
using PetClinicApp.Source.Modules.Reminders.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RemindersController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromServices] FindReminderService findReminderService, long id)
        {
            var reminder = await findReminderService.ExecuteAsync(id);

            return Ok(reminder);
        }

        [HttpGet("pet/{petId}")]
        public async Task<ActionResult<List<Reminder>>> Get([FromServices] ListRemindersService listRemindersService,[FromRoute] long petId, [FromQuery] string search)
        {
            var reminders = await listRemindersService.ExecuteAsync(petId, search);

            return Ok(reminders);
        }

        [HttpPost]
        public async Task<ActionResult<Reminder>> Post([FromServices] CreateReminderService createReminderService, ReminderDTO reminder)
        {
            var createdReminder = await createReminderService.ExecuteAsync(reminder);

            return CreatedAtAction("Get", new {Id = createdReminder.Id}, createdReminder);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateReminderService updateReminderService, [FromBody] ReminderDTO reminder)
        {
            await updateReminderService.ExecuteAsync(reminder);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Reminder>> Delete([FromServices] DeleteReminderService deleteReminderService, [FromRoute] long id)
        {
            var reminder = await deleteReminderService.ExecuteAsync(id);

            return Ok(reminder);
        }
    }
}
