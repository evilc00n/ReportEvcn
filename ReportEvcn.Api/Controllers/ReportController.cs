using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using System.Security.Claims;

namespace ReportEvcn.Api.Controllers
{


    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Получение отчётов пользователя
        /// </summary>
        /// <response code="200">Если отчёты были получены</response>
        /// <response code="400">Если отчёты не были получены</response>
        [HttpGet("reports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> GetUserReports()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var responce = await _reportService.GetReportsAsync(new Guid(userId));
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }


        /// <summary>
        /// Получение отчётов с указанием идентификатора
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     GET
        ///     {
        ///         "id": "1"
        ///     }
        /// </remarks>
        /// <response code="200">Если отчёт был получен</response>
        /// <response code="400">Если отчёт не был получен</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> GetReport(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var responce = await _reportService.GetReportByIdAsync(id, new Guid(userId));
            if (responce.IsSuccess)
            {
               return Ok(responce);
            }
            return BadRequest(responce);
        }



        /// <summary>
        /// Удаление отчёта с указанием идентификатора
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
        /// <response code="200">Если отчёт удалился</response>
        /// <response code="400">Если отчёт не был удалён</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> DeleteReport(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var responce = await _reportService.DeleteReportAsync(id, new Guid(userId));
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }



        /// <summary>
        /// Создание отчёта
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     POST
        ///     {
        ///         "name": "Report1",
        ///         "description": "Test report",
        ///         "userId": "1"
        ///     }
        /// </remarks>
        /// <response code="200">Если отчёт создался</response>
        /// <response code="400">Если отчёт не был создан</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> CreateReport([FromBody]CreateReportDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var responce = await _reportService.CreateReportAsync(dto, new Guid(userId));
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }

        /// <summary>
        /// Обновление отчёта с указанием основных свойств
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     PUT
        ///     {
        ///         "id": "1"  
        ///         "name": "Report2",
        ///         "description": "Test report2",
        ///     }
        /// </remarks>
        /// <response code="200">Если отчёт обновился</response>
        /// <response code="400">Если отчёт не обновился</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> UpdateReport([FromBody] UpdateReportDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var responce = await _reportService.UpdateReportAsync(dto, new Guid(userId));
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }
    }
}
