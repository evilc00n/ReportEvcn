using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Dto.User;
using ReportEvcn.Domain.Result;

namespace ReportEvcn.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис, предназначенный для авторизации/регистрации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserDTO>> Register(RegisterUserDTO dto);

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<TokenDTO>> Login(LoginUserDTO dto);

    }
}
