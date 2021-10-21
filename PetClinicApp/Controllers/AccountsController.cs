using Microsoft.AspNetCore.Mvc;
using PetClinicApp.Source.Modules.Accounts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PetClinicApp.Source.Modules.Accounts.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PetClinicApp.Source.Modules.Accounts.Models;
using PetClinicApp.Source.Shared.Jwt;
using Microsoft.AspNetCore.Http;
using PetClinicApp.Source.Shared.Errors;
using System.IO;

namespace PetClinicApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountsController : ControllerBase
    {

        /// <summary>
        /// Registrar um usuário
        /// </summary>
        /// <param name="registerUserService"></param>
        /// <param name="registerUser">Modelo para registrar um usuário</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 500)]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromServices] RegisterUserService registerUserService, [FromBody] RegisterUserDTO registerUser)
        {
            await registerUserService.ExecuteAsync(registerUser);

            return NoContent();
        }

        /// <summary>
        /// Atualizar um usuário
        /// </summary>
        /// <param name="updateUserService"></param>
        /// <param name="account">Modelo para atualizar um usuário</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 500)]
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update([FromServices] UpdateUserService updateUserService, [FromBody] UpdateAccountDTO account)
        {
            long loggedUserId = User.GetUserId();

            await updateUserService.ExecuteAsync(account, loggedUserId);

            return NoContent();
        }

        /// <summary>
        /// Atualizar a senha do usuário
        /// </summary>
        /// <param name="updatePasswordService"></param>
        /// <param name="updatePassword">Modelo para atualizar a senha do usuário</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 500)]
        [Authorize]
        [HttpPut("update-password")]
        public async Task<ActionResult> UpdatePassword([FromServices] UpdatePasswordService updatePasswordService,[FromBody] UpdatePasswordDTO updatePassword)
        {
            long loggedUserId = User.GetUserId();

            await updatePasswordService.ExecuteAsync(loggedUserId, updatePassword);

            return NoContent();
        }

        /// <summary>
        /// Atualizar o avatar do usuário
        /// </summary>
        /// <param name="uploadAccountAvatarService"></param>
        /// <param name="file">Arquivo contendo o avatar do usuário. .png .jpg .jpeg</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 500)]
        [Authorize]
        [HttpPost("avatar")]
        public async Task<ActionResult> UploadAvatar([FromServices] UploadAccountAvatarService uploadAccountAvatarService, [FromForm] IFormFile file)
        {
            long loggedUserId = User.GetUserId();

            await uploadAccountAvatarService.ExecuteAsync(loggedUserId, file);

            return NoContent();
        }

        /// <summary>
        /// Retorna o avatar do usuário logado
        /// </summary>
        /// <param name="downloadAccountAvatarService"></param>
        /// <returns>Imagem do usuário logado</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(AppError), 400)]
        [ProducesResponseType(typeof(AppError), 404)]
        [ProducesResponseType(typeof(AppError), 500)]
        [Authorize]
        
        [HttpGet("avatar")]
        public async Task<ActionResult> DownloadAvatar([FromServices] DownloadAccountAvatarService downloadAccountAvatarService)
        {
            long loggedUserId = User.GetUserId();

            var fileModel = await downloadAccountAvatarService.ExecuteAsync(loggedUserId);

            return File(fileModel.Buffer, fileModel.MimeType);
        }
    }
}
