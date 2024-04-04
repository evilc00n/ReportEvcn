
using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Result;

namespace ReportEvcn.Domain.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Получение данных о пользователе по его accessToken в TokenDTO
        /// </summary>
        /// <returns></returns>
        Task<BaseResult<UserDTO>> GetUserDataAsync(Guid userId);
    }
}
