using GameDashboard.Application.Abstractions;
using GameDashboard.Application.Constants;
using GameDashboard.Domain.Concrete.Identity;
using GameDashboard.Infrastructure.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace GameDashboard.Infrastructure
{
    public class JWTTokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<AppUser> _userManager;

        public JWTTokenService(TokenSettings tokenSettings, UserManager<AppUser> userManager)
        {
            _tokenSettings = tokenSettings;
            _userManager = userManager;
        }

        //Nurada jwt token olusturuldu.
        public async Task<JwtSecurityToken> CreateToken(AppUser user, IList<string> roles)
        {
            // Jwt token uretilirken icinde hangi claimslerin bulunacagi yazdik tokenle beraber gonderilecek bilgileri ekledik.
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Name, $"{user.UserName}"),
            };
            //claims tarafina rolleri de ekledik birden fazla rolu olebileceg icin liste seklinde yapildi.
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            //Token icindeki sifremizi ekledik bu sifreye gore token uretimi saglanacak.

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));

            //Token uretimi yapilmaya baslandi.

            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,//Sagliyici belirlendi
                audience: _tokenSettings.Audience,//Kullanici belielendi
                expires: DateTime.Now.AddMinutes(_tokenSettings.Expiration),//Gecerlilik suresi belirlendi
                claims: claims,//claimsler eklendi
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//Sifre Sh256 standartinda gizlendi
                );

            await _userManager.AddClaimsAsync(user, claims); //Data base eklendi

            return token;
        }

        public ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey)),
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(ResponseMessages.TokenNotFound);
            }
            return principal;
        }
    }
}
