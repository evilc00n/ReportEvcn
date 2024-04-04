using ReportEvcn.Application.Resources;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Enum;
using ReportEvcn.Domain.Interfaces.Validations;
using ReportEvcn.Domain.Result;
using Serilog;

namespace ReportEvcn.Application.Validations
{
    public class ReportValidator : IReportValidator
    {
        private readonly ILogger _logger;

        public ReportValidator(ILogger logger)
        {
            _logger = logger;
        }

        public BaseResult CreateValidator(Report report, User user)
        {
            if (report != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportAlreadyExists,
                    ErrorCode = (int) ErrorCodes.ReportAlreadyExists
                };
            }
            if (user == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            return new BaseResult();
        }

        public BaseResult ValidateOnNull(Report model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                };
            }
            return new BaseResult();
        }

    }
}
