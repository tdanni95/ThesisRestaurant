using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Create
{
    public class CreateIngredientTypeCommandValidator : AbstractValidator<CreateIngredientTypeCommand>
    {
        public CreateIngredientTypeCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.MinOption).GreaterThanOrEqualTo(0);
            RuleFor(c => c.MaxOption).GreaterThanOrEqualTo(x => x.MinOption);
        }
    }
}
