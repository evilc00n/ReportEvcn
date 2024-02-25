using ReportEvcn.Domain.Dto;
using ReportEvcn.Domain.Result;
using System.Security.Claims;

namespace ReportEvcn.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

        Task<BaseResult<TokenDTO>> RefreshToken(TokenDTO dto);

    }
}
