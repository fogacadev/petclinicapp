using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MedicalHistoryTypesController :  ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryTypeDTO>> Get([FromServices] FindMedicalHistoryTypeService findMedicalHistoryTypeService, [FromRoute] long id)
        {
            var type = await findMedicalHistoryTypeService.ExecuteAsync(id);

            return Ok(type);
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalHistoryTypeDTO>>> Get([FromServices] ListMedicalHistoryTypeService listMedicalHistoryTypeService, [FromQuery] string search)
        {
            var types = await listMedicalHistoryTypeService.ExecuteAsync(search);

            return Ok(types);
        }
    }
}
