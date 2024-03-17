using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Application.Services;
using ReportEvcn.Domain.Dto.Role;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using System.Net.Mime;

namespace ReportEvcn.Api.Controllers
{

    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     POST
        ///     {
        ///         "name": "Admin"
        ///     }
        /// </remarks>
        /// <response code="200">Если отчёт создался</response>
        /// <response code="400">Если отчёт не был создан</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> Create([FromBody] CreateRoleDTO dto)
        {
            var responce = await _roleService.CreateRoleAsync(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }


        /// <summary>
        /// Удаление роли с указанием идентификатора
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     DELETE
        ///     {
        ///         "id": "1"
        ///     }
        /// </remarks>
        /// <response code="200">Если роль удалилась</response>
        /// <response code="400">Если роль не была удалена</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> Delete(long id)
        {
            var responce = await _roleService.DeleteRoleAsync(id);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }


        /// <summary>
        /// Обновление роли с указанием основных свойств
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     PUT
        ///     {
        ///         "id": "1"  
        ///         "name": "Admin"
        ///     }
        /// </remarks>
        /// <response code="200">Если роль обновилась</response>
        /// <response code="400">Если роль не обновилась</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> Update([FromBody] UpdateRoleDTO dto)
        {
            var responce = await _roleService.UpdateRoleAsync(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }



        /// <summary>
        /// Добавление роли пользователю
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     POST
        ///     {
        ///         "Login": "User1"
        ///         "RoleName": "Admin"
        ///     }
        /// </remarks>
        /// <response code="200">Если роль была добавлена пользователю</response>
        /// <response code="400">Если роль не была добавлена пользователю</response>
        [HttpPost("addRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> AddRoleForUser([FromBody] UserRoleDTO dto)
        {
            var responce = await _roleService.AddRoleForUserAsync(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }
    }
}
