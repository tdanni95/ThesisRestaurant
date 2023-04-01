using FluentValidation;

namespace ThesisRestaurant.Application.Foods.Commands.Update
{
    public class UpdateFoodCommandValidator : AbstractValidator<UpdateFoodCommand>
    {
        public UpdateFoodCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.BasePrice).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FoodTypeId).NotEmpty().GreaterThan(0);
        }
    }
}
