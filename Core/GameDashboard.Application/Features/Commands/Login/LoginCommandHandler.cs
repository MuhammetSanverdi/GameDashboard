
using GameDashboard.Application.Abstractions;
using GameDashboard.Application.Constants;
using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace GameDashboard.Application.Features.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, IDataResult<LoginCommandResponse>>
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private ITokenService _tokenService;

        public LoginCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<IDataResult<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByNameAsync(request.UserName);

            if (appUser is null)
            {
                return new ErrorDataResult<LoginCommandResponse>(ResponseMessages.UsernameOrPasswordInvalid);   
            }
            var roles = await _userManager.GetRolesAsync(appUser);

            var checkPassword = await _userManager.CheckPasswordAsync(appUser, request.Password);

            if (!checkPassword)
            {
                return new ErrorDataResult<LoginCommandResponse>(ResponseMessages.UsernameOrPasswordInvalid);
            }

            var securityToken = await _tokenService.CreateToken(appUser, roles);

            var _token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            var response = new LoginCommandResponse()
            {
                AccessToken = _token,
                ExpirationTime = securityToken.ValidTo,
            };
            return new SuccessDataResult<LoginCommandResponse>(response);
        }
    }
}
