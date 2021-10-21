using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.MedicalHistories.DTO;
using PetClinicApp.Source.Modules.MedicalHistories.Entities;
using PetClinicApp.Source.Modules.MedicalHistories.Services;
using PetClinicApp.Source.Shared.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MedicalHistoryTypesController :  ControllerBase
    {
        /// <summary>
        /// Retorna um tipo de histórico
        /// </summary>
        /// <param name="findMedicalHistoryTypeService"></param>
        /// <param name="id">Id do tipo de histórico</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MedicalHistoryTypeDTO), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryTypeDTO>> Get([FromServices] FindMedicalHistoryTypeService findMedicalHistoryTypeService, [FromRoute] long id)
        {
            var type = await findMedicalHistoryTypeService.ExecuteAsync(id);

            return Ok(type);
        }

        /// <summary>
        /// Retorna uma lista de tipo de histórico
        /// </summary>
        /// <param name="listMedicalHistoryTypeService"></param>
        /// <param name="search">Pesquisar</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<MedicalHistoryTypeDTO>), 200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [HttpGet]
        public async Task<ActionResult<List<MedicalHistoryTypeDTO>>> Get([FromServices] ListMedicalHistoryTypeService listMedicalHistoryTypeService, [FromQuery] string search)
        {
            var types = await listMedicalHistoryTypeService.ExecuteAsync(search);

            return Ok(types);
        }
    }
}
