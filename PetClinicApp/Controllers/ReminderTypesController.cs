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
    public class ReminderTypesController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderTypeDTO>> Get([FromServices] FindReminderTypeService findReminderTypeService, [FromRoute] long id)
        {
            var type = await findReminderTypeService.ExecuteAsync(id);

            return Ok(type);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReminderTypeDTO>>> Get([FromServices] ListReminderTypesService listReminderTypesService, [FromQuery] string search)
        {
            var types = await listReminderTypesService.ExecuteAsync(search);

            return Ok(types);
        }

        [HttpPost]
        public async Task<ActionResult<ReminderTypeDTO>> Post([FromServices] CreateReminderTypeService createReminderTypeService, CreateReminderTypeDTO reminderType)
        {
            var type = await createReminderTypeService.ExecuteAsync(reminderType);

            return CreatedAtAction("Get", new { Id = type.Id }, type);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromServices] UpdateReminderTypeService updateReminderTypeService, UpdateReminderTypeDTO reminderType)
        {
            await updateReminderTypeService.ExecuteAsync(reminderType);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReminderTypeDTO>> Delete([FromServices] DeleteReminderTypeService deleteReminderTypeService, [FromRoute] long id)
        {
            var type = await deleteReminderTypeService.ExecuteAsync(id);

            return Ok(type);
        }

    }
}
