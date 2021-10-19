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
    [Route("api/v1/[controller]")]
    public class ReminderTypesController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderType>> Get([FromServices] FindReminderTypeService findReminderTypeService, [FromRoute] long id)
        {
            var type = await findReminderTypeService.ExecuteAsync(id);

            return Ok(type);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReminderType>>> Get([FromServices] ListReminderTypesService listReminderTypesService, [FromQuery] string search)
        {
            var types = await listReminderTypesService.ExecuteAsync(search);

            return Ok(types);
        }

        [HttpPost]
        public async Task<ActionResult<ReminderType>> Post([FromServices] CreateReminderTypeService createReminderTypeService, ReminderTypeDTO reminderType)
        {
            var type = await createReminderTypeService.ExecuteAsync(reminderType);

            return CreatedAtAction("Get", new { Id = type.Id }, type);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateReminderTypeService updateReminderTypeService, ReminderTypeDTO reminderType)
        {
            await updateReminderTypeService.ExecuteAsync(reminderType);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReminderType>> Delete([FromServices] DeleteReminderTypeService deleteReminderTypeService, [FromRoute] long id)
        {
            var loggedUserId = User.GetUserId();
            var type = await deleteReminderTypeService.ExecuteAsync(loggedUserId, id);

            return Ok(type);
        }

    }
}
