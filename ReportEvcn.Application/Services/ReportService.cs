using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Repositories;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Interfaces.Validations;
using ReportEvcn.Domain.Result;
using Serilog;
using System.Resources;
using System.Runtime.Versioning;

namespace ReportEvcn.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IReportValidator _reportValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ReportService(IBaseRepository<Report> reportRepository, IBaseRepository<User> userRepository,
            ILogger logger, IReportValidator reportValidator, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
            _logger = logger;
            _reportValidator = reportValidator;
            _mapper = mapper;
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
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }
            
            if (!reports.Any())
            {
                _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
                return new CollectionResult<ReportDTO>
                {
                    ErrorMessage = ErrorMessage.ReportsNotFound,
                    ErrorCode = (int)ErrorCodes.ReportsNotFound
                };
            }

            return new CollectionResult<ReportDTO>
            {
                Data = reports,
                Count = reports.Length
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDTO>> GetReportByIdAsync(long id)
        {
            ReportDTO? report;
            

            try
            {
                report = await _reportRepository.GetAll()
                    .Select(x => new ReportDTO(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefaultAsync(x => x.Id == id);  
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDTO>
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }

            if (report == null)
            {
                _logger.Warning("Report with {id} was not found", id);
                return new BaseResult<ReportDTO>
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                };
            }

            return new BaseResult<ReportDTO>
            {
                Data = report
            };
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDTO>> CreateReportAsync(CreateReportDTO dto)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
                var result = _reportValidator.CreateValidator(report, user);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDTO>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,   
                    };
                }
                report = new Report()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UserId = user.Id
                };
                await _reportRepository.CreateAsync(report);

                return new BaseResult<ReportDTO>()
                {
                    Data = _mapper.Map<ReportDTO>(report)
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDTO>
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }

            throw new NotImplementedException();
        }

        public async Task<BaseResult<ReportDTO>> DeleteReportAsync(long id)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _reportValidator.ValidateOnNull(report);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDTO>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                }
                await _reportRepository.RemoveAsync(report);
                return new BaseResult<ReportDTO>()
                {
                    Data = _mapper.Map<ReportDTO>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDTO>
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }
        }

        public async Task<BaseResult<ReportDTO>> UpdateReportAsync(UpdateReportDTO dto)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
                var result = _reportValidator.CreateValidator(report, user);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDTO>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                }

                report.Name = dto.Name;
                report.Description = dto.Description;
                await _reportRepository.UpdateAsync(report);
                return new BaseResult<ReportDTO>()
                {
                    Data = _mapper.Map<ReportDTO>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDTO>
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternarServerError
                };
            }
        }
    }
}
