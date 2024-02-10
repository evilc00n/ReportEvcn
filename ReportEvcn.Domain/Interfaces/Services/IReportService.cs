using ReportEvcn.Domain.Dto.Report;
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
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CollectionResult<ReportDTO>> GetReportsAsync(long userId);

        /// <summary>
        /// Получение отчёта по индентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> GetReportByIdAsync(long id);

        /// <summary>
        /// Создание отчёта с базовыми параметрами.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> CreateReportAsync(CreateReportDTO dto);

        /// <summary>
        /// Удаление отчёта по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> DeleteReportAsync(long id);

        /// <summary>
        /// Обновление отчёта.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<ReportDTO>> UpdateReportAsync(UpdateReportDTO dto);
    }
}
