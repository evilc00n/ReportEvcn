using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ReportEvcn.Application.Mapping;
using ReportEvcn.Application.Services;
using ReportEvcn.Application.Validations;
using ReportEvcn.Application.Validations.FluentFalidations.Report;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Interfaces.Services;
using ReportEvcn.Domain.Interfaces.Validations;

namespace ReportEvcn.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplications(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportMapping));
            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IReportValidator, ReportValidator>();
            services.AddScoped<IValidator<CreateReportDTO>, CreateReportValidator>();
            services.AddScoped<IValidator<UpdateReportDTO>, UpdateReportValidator>();
            services.AddScoped<IReportService, ReportService>();
        }
    }
}
