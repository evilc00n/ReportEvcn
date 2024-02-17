
using Microsoft.AspNetCore.Mvc;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;

namespace ReportEvcn.Api.Controllers
{
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

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
