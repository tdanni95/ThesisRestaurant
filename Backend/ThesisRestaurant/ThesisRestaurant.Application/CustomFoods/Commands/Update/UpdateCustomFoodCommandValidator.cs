using FluentValidation;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Update
{
    public class UpdateCustomFoodCommandValidator : AbstractValidator<UpdateCustomFoodCommand>
    {
        public UpdateCustomFoodCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FoodTypeId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.IngredientIds).NotEmpty();
            RuleForEach(x => x.IngredientIds).NotEmpty().GreaterThan(0);
        }
    }
}
