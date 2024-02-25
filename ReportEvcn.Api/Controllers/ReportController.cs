using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;

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
        /// <param name="userId"></param>
        /// <remarks>
        /// Sample request:
        ///  
        ///     GET
        ///     {
        ///         "userId": "1"
        ///     }
        /// </remarks>
        /// <response code="200">Если отчёт создался</response>
        /// <response code="400">Если отчёт не был создан</response>
        [HttpGet("reports/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> GetUserReports(long userId)
        {
            var responce = await _reportService.GetReportsAsync(userId);
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
        /// <response code="200">Если отчёт создался</response>
        /// <response code="400">Если отчёт не был создан</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> GetReport(long id)
        {
            var responce = await _reportService.GetReportByIdAsync(id);
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
        /// <response code="200">Если отчёт создался</response>
        /// <response code="400">Если отчёт не был создан</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> DeleteReport(long id)
        {
            var responce = await _reportService.DeleteReportAsync(id);
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
            var responce = await _reportService.CreateReportAsync(dto);
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
        /// <response code="200">Если отчёт создался</response>
        /// <response code="400">Если отчёт не был создан</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReportDTO>>> UpdateReport([FromBody] UpdateReportDTO dto)
        {
            var responce = await _reportService.UpdateReportAsync(dto);
            if (responce.IsSuccess)
            {
                return Ok(responce);
            }
            return BadRequest(responce);
        }
    }
}
