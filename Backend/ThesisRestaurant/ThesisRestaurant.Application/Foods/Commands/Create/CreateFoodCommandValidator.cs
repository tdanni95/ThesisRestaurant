using FluentValidation;

namespace ThesisRestaurant.Application.Foods.Commands.Create
{
    public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
    {
        public CreateFoodCommandValidator()
        {
            RuleFor(x => x.BasePrice).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FoodTypeId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.IngredientIds).NotEmpty();
            RuleForEach(x => x.IngredientIds).NotEmpty().GreaterThan(0);
        }
    }
}
