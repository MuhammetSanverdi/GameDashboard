using FluentValidation;
using GameDashboard.Application.Constants;
using GameDashboard.Application.Features.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Validations
{
    public class RegisterCommandRequestValidator:AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandRequestValidator()
        {
            RuleFor(r => r.UserName)
                .MinimumLength(4)
                .WithMessage(ValidationMessages.UsernameMinLengthError);

            RuleFor(r=>r.Email)
                .EmailAddress()
                .WithMessage(ValidationMessages.EmailAddressInvalid);

            RuleFor(r => r.Password)
                .Equal(r => r.ConfirmPassword)
                .WithMessage(ValidationMessages.PasswordsNotMatched);

                RuleFor(r => r.Password)
            .MinimumLength(8).WithMessage(ValidationMessages.PasswordMinimumLengthEight)
            .Matches("[A-Z]")
            .Matches("[a-z]")
            .Matches("[0-9]")
            .Matches(@"[\p{P}\p{S}]").WithMessage(ValidationMessages.PasswordRegexError);
        }
    }
}
