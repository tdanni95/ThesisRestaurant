using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Ingredients.Commands.Create
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(x=> x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);

            RuleFor(x => x.IngredientTypeId).NotEmpty().GreaterThan(0);
        }
    }
}
