using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEvcn.Domain.Interfaces.Services
{

    /// <summary>
    /// Сервис, отвечающий за работу с доменной частью отчёта (Report)
    /// </summary>

    public interface IReportService
    {
        /// <summary>
        /// Получение всех отчётов пользователя.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CollectionResult<ReportDTO>> GetReportsAsync(long userId);
    }
}
