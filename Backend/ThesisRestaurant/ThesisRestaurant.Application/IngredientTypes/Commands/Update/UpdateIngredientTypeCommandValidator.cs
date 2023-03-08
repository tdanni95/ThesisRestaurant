using FluentValidation;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Update
{
    public class UpdateIngredientTypeCommandValidator : AbstractValidator<UpdateIngredientTypeCommand>
    {
        public UpdateIngredientTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(c => c.MinOption).NotEmpty().GreaterThan(0);
            RuleFor(c => c.MaxOption).NotEmpty().GreaterThanOrEqualTo(x => x.MinOption);
        }
    }
}
