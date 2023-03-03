using FluentValidation;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Update
{
    public class UpdateIngredientTypeCommandValidator : AbstractValidator<UpdateIngredientTypeCommand>
    {
        public UpdateIngredientTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).GreaterThan(0);

        }
    }
}
