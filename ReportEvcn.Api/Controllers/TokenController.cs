using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;

namespace ReportEvcn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<BaseResult<TokenDTO>>> RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            var responce = await _tokenService.RefreshToken(tokenDTO);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }

    }
}
