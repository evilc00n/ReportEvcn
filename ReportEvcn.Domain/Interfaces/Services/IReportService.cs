using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.Report;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Result;


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
        /// <param name="login"></param>
        /// <returns></returns>
        Task<CollectionResult<ReportDTO>> GetReportsAsync(Guid userId);

        /// <summary>
        /// Получение отчёта по индентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> GetReportByIdAsync(Guid id, Guid userId);

        /// <summary>
        /// Создание отчёта с базовыми параметрами.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> CreateReportAsync(CreateReportDTO dto, Guid userId);

        /// <summary>
        /// Удаление отчёта по id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> DeleteReportAsync(Guid id, Guid userId);

        /// <summary>
        /// Обновление отчёта.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> UpdateReportAsync(UpdateReportDTO dto, Guid userId);
    }
}
