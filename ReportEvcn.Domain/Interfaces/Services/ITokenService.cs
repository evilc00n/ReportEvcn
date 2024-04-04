using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Result;
using System.Security.Claims;

namespace ReportEvcn.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Генерация accessToken на основе claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Генерация RefreshToken
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Получение ClaimsPrincipal access токена
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

        /// <summary>
        /// Обновление acess и refresh токена по access токену
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<TokenDTO>> RefreshToken(TokenDTO dto);



    }
}
