using FluentValidation;
using GameDashboard.Application.Abstractions;
using GameDashboard.Application.Constants;
using GameDashboard.Application.Results;
using GameDashboard.Domain.Concrete.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameDashboard.Application.Features.Commands.Register
{
    /// <summary>
    /// User register request
    /// </summary>
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, IResult>
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IMapper _mapper;
        private readonly IValidator<RegisterCommandRequest> _validator;
        public RegisterCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, IValidator<RegisterCommandRequest> validator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<IResult> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new ErrorResult(validationResult.Errors[0].ErrorMessage);
            }
            var existUser = _userManager.Users.SingleOrDefault(u => u.Email == request.Email || u.UserName == request.UserName);

            if (existUser is not null)
            {
                return new ErrorResult(ResponseMessages.UserExist);
            }

            var user = _mapper.Map<AppUser>(request);

            user.NormalizedUserName = request.UserName.ToUpperInvariant();

            user.SecurityStamp = Guid.NewGuid().ToString();

            user.EmailConfirmed = true;

            user.NormalizedEmail = request.Email.ToUpperInvariant();

            var result = await _userManager.CreateAsync(user,request.Password);

            var role = await _userManager.AddToRoleAsync(user,"User");

            return new SuccessResult(ResponseMessages.UserCreated);

        }
    }
}
