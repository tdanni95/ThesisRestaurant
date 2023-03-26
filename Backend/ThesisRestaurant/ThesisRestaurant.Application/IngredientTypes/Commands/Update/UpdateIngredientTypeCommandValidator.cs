using FluentValidation;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Update
{
    public class UpdateIngredientTypeCommandValidator : AbstractValidator<UpdateIngredientTypeCommand>
    {
        public UpdateIngredientTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(c => c.MinOption).GreaterThanOrEqualTo(0);
            RuleFor(c => c.MaxOption).GreaterThanOrEqualTo(x => x.MinOption);
        }
    }
}
