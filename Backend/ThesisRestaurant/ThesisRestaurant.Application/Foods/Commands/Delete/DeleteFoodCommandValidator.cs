using FluentValidation;

namespace ThesisRestaurant.Application.Foods.Commands.Delete
{
    public class DeleteFoodCommandValidator : AbstractValidator<DeleteFoodCommand>
    {
        public DeleteFoodCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().GreaterThan(0);
        }
    }
}
