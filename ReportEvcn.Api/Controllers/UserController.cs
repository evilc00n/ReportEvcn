using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Application.Services;
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using System.Security.Claims;

namespace ReportEvcn.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        /// <response code="200">Если данные были получены</response>
        /// <response code="400">Если данные не были получены</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<UserDTO>>> GetUserData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var responce = await _userService.GetUserDataAsync(new Guid(userId));
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }

            

            return BadRequest(responce);
        }
    }
}
