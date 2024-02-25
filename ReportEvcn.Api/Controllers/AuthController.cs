
using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Interfaces.Services;

namespace ReportEvcn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterUserDTO dto)
        {
            var responce = await _authService.Register(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("login")]
        public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginUserDTO dto)
        {
            var responce = await _authService.Login(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }


    }
}
