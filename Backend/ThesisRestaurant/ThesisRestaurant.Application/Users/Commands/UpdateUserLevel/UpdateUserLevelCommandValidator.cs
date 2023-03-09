using FluentValidation;

namespace ThesisRestaurant.Application.Users.Commands.UpdateUserLevel
{
    public class UpdateUserLevelCommandValidator : AbstractValidator<UpdateUserLevelCommand>
    {
        public UpdateUserLevelCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Level).NotEmpty();
        }
    }
}
