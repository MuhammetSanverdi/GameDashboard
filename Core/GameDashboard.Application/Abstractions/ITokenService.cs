using GameDashboard.Domain.Concrete.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameDashboard.Application.Abstractions
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles);
        ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string token);
    }
}
