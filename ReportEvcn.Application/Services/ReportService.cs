using Microsoft.EntityFrameworkCore;
using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Repositories;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Result;
using Serilog;
using System.Resources;
using System.Runtime.Versioning;

namespace ReportEvcn.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private ILogger _logger;

        public ReportService(IBaseRepository<Report> reportRepository, ILogger logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<CollectionResult<ReportDTO>> GetReportsAsync(long userId)
        {
            ReportDTO[] reports;

            try
            {
                reports = await _reportRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new ReportDTO(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<ReportDTO>
                {
                    ErrorMesage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }
            
            if (!reports.Any())
            {
                _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
                return new CollectionResult<ReportDTO>
                {
                    ErrorMesage = ErrorMessage.ReportsNotFound,
                    ErrorCode = (int)ErrorCodes.ReportsNotFound
                };
            }

            return new CollectionResult<ReportDTO>
            {
                Data = reports,
                Count = reports.Length
            };
        }
    }
}
