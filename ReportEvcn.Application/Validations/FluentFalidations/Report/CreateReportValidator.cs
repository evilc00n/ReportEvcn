using FluentValidation;
using ReportEvcn.Domain.Dto.Report;

namespace ReportEvcn.Application.Validations.FluentFalidations.Report
{
    public class CreateReportValidator : AbstractValidator<CreateReportDTO>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        }


    }
}
