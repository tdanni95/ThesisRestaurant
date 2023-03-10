using FluentValidation;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Create
{
    public class CreateCustomFoodRequestValidator : AbstractValidator<CreateCustomFoodCommand>
    {
        public CreateCustomFoodRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FoodTypeId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.IngredientIds).NotEmpty();
            RuleForEach(x => x.IngredientIds).NotEmpty().GreaterThan(0);
        }
    }
}
