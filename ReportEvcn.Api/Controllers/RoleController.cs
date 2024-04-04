using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Application.Services;
using ReportEvcn.Domain.Dto.Role;
using ReportEvcn.Domain.Dto.UserRole;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using System.Net.Mime;

namespace ReportEvcn.Api.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> GetAllRoles()
        {
            var responce = await _roleService.GetAllRolesAsync();
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
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
        /// <response code="200">Если роль удалилась</response>
        /// <response code="400">Если роль не была удалена</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> Delete(Guid id)
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
        ///         "RoleId": "3"
        ///     }
        /// </remarks>
        /// <response code="200">Если роль была добавлена пользователю</response>
        /// <response code="400">Если роль не была добавлена пользователю</response>
        [HttpPost("add-role")]
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




        /// <summary>
        /// Удалена роли у пользователю
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     DELETE
        ///     {
        ///         "Login": "User1"
        ///         "RoleName": "Admin"
        ///     }
        /// </remarks>
        /// <response code="200">Если роль у пользователя была удалена</response>
        /// <response code="400">Если роль у пользователя не была удалена</response>
        [HttpDelete("delete-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> DeleteRoleForUser([FromBody] DeleteUserRoleDTO dto)
        {
            var responce = await _roleService.DeleteRoleForUserAsync(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }


        /// <summary>
        /// Обновление роли у пользователю
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT
        ///     {
        ///         "Login": "User1"
        ///         "OldRoleId": "2"
        ///         "NewRoleId": "3"
        ///     }
        /// </remarks>
        /// <response code="200">Если роль у пользователя была обновлена</response>
        /// <response code="400">Если роль у пользователя не была обновлена</response>
        [HttpPut("update-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDTO>>> UpdateRoleForUser([FromBody] UpdateUserRoleDTO dto)
        {
            var responce = await _roleService.UpdateRoleForUserAsync(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }

    }
}
