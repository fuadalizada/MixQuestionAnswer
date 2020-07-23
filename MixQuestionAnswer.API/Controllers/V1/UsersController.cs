using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MixQuestionAnswer.API.Contract.V1;
using MixQuestionAnswer.API.Controllers.Base;
using MixQuestionAnswer.BLL.DTOs;
using MixQuestionAnswer.BLL.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace MixQuestionAnswer.API.Controllers.V1
{
    public class UsersController : MainController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get All for User
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpGet(ApiRoute.Users.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            if (result.IsSucceed)
                return Ok(result);
            else
                return BadRequest(result.FailureResult);
        }

        [HttpGet(ApiRoute.Users.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid Id)
        {
            var user = await _userService.GetByIdAsync(Id);
            if (user.IsSucceed)
                return Ok(user);
            return BadRequest(user.FailureResult);
        }

        [HttpPost(ApiRoute.Users.Register)]
        public async Task<IActionResult> Register([FromBody]UserDTO model)
        {
            var created = await _userService.SaveAsync(model);
            if (created.IsSucceed)
            {
                string baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string createdUri = baseUri + "/" + ApiRoute.Users.Get.Replace("{Id}", created.Data.ToString());
                return Created(createdUri, model);
            }
            else
                return BadRequest(created.FailureResult);
        }

        [HttpPut(ApiRoute.Users.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid Id,[FromBody]UserDTO model)
        {
            model.Id = Id;
            var updated = await _userService.SaveAsync(model);
            if (updated.IsSucceed)
                return Ok();
            return BadRequest(updated.FailureResult);

        }

        [HttpDelete(ApiRoute.Users.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid Id)
        {
            var deleted = await _userService.RemoveAsync(Id);
            if (deleted.IsSucceed)
                return Ok();
            return BadRequest(deleted.FailureResult);

        }

    }
}