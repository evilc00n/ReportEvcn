
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




    }
}
