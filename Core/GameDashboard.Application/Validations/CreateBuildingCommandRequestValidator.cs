using FluentValidation;
using GameDashboard.Application.Constants;
using GameDashboard.Application.Features.Commands.CreateBuilding;

namespace GameDashboard.Application.Validations
{
    public class CreateBuildingCommandRequestValidator : AbstractValidator<CreateBuildingCommandRequest>
    {
        public CreateBuildingCommandRequestValidator()
        {
            RuleFor(r => r.ConstructionTime)
                .LessThanOrEqualTo(1800)
                .GreaterThanOrEqualTo(30)
                .WithMessage(ValidationMessages.ConstructionTimeNotRange);

            RuleFor(r => r.Cost)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.BuildingCostNotLessThenZero);

        }
    }
}
