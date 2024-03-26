using ReportEvcn.Domain.Dto.Role;
using ReportEvcn.Domain.Dto.UserRole;
using ReportEvcn.Domain.Entity;
using ReportEvcn.Domain.Result;

namespace ReportEvcn.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис, предназначенный для управления ролями
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoleDTO>> CreateRoleAsync(CreateRoleDTO dto);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<RoleDTO>> DeleteRoleAsync(long id);

        /// <summary>
        /// Обновление роли
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<RoleDTO>> UpdateRoleAsync(UpdateRoleDTO dto);

        /// <summary>
        /// Добавление роли для пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<UserRoleDTO>> AddRoleForUserAsync(UserRoleDTO dto);

        /// <summary>
        /// Удаление роли у пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserRoleDTO>> DeleteRoleForUserAsync(DeleteUserRoleDTO dto);


        /// <summary>
        /// Обновление роли у пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserRoleDTO>> UpdateRoleForUserAsync(UpdateUserRoleDTO dto);
    }
}
