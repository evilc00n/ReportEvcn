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
        public async Task<CollectionResult<ReportDTO>> GetReportsAsync(Guid userId)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                _logger.Error(ErrorMessage.UserNotFound);
                return new CollectionResult<ReportDTO>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            ReportDTO[] reports;
            reports = await _reportRepository.GetAll()
                .Where(x => x.UserId == user.Id)
                .Select(x => _mapper.Map<ReportDTO>(x))
                .ToArrayAsync();
            
            if (!reports.Any())
            {
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
        public async Task<BaseResult<ReportDTO>> GetReportByIdAsync(Guid id, Guid userId)
        {

            ReportDTO? report;
            report = await _reportRepository.GetAll()
                .Where(x => x.Id == id && x.UserId == userId)
                .Select(x => _mapper.Map<ReportDTO>(x))
                .FirstOrDefaultAsync();


            if (report == null)
            {
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
        public async Task<BaseResult<ReportDTO>> CreateReportAsync(CreateReportDTO dto, Guid userId)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == userId);
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            var result = _reportValidator.CreateValidator(report, user);
            if (!result.IsSuccess)
            {
                if(result.ErrorCode == (int)ErrorCodes.UserNotFound)
                {
                    _logger.Error(ErrorMessage.UserNotFound);
                }

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
            await _reportRepository.SaveChangesAsync();

            return new BaseResult<ReportDTO>()
            {
                Data = _mapper.Map<ReportDTO>(report)
            };

        }

        public async Task<BaseResult<ReportDTO>> DeleteReportAsync(Guid id, Guid userId)
        {
            var report = await _reportRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            var result = _reportValidator.ValidateOnNull(report);
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDTO>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                };
            }
            _reportRepository.Remove(report);
            await _reportRepository.SaveChangesAsync();
            return new BaseResult<ReportDTO>()
            {
                Data = _mapper.Map<ReportDTO>(report)
            };
        }

        public async Task<BaseResult<ReportDTO>> UpdateReportAsync(UpdateReportDTO dto, Guid userId)
        {
            var report = await _reportRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id && x.UserId == userId);
            var result = _reportValidator.ValidateOnNull(report);
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
            var updatedReport = _reportRepository.Update(report);
            await _reportRepository.SaveChangesAsync();
            return new BaseResult<ReportDTO>()
            {
                Data = _mapper.Map<ReportDTO>(updatedReport)
            };
        }
    }
}
