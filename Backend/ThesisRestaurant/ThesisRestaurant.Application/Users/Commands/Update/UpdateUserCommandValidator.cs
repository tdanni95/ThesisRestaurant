using FluentValidation;

namespace ThesisRestaurant.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty().GreaterThan(0);
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.PhoneNumber).NotEmpty();
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
        }
    }
}
