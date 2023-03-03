using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.IngredientTypes.Commands.Delete
{
    public class DeleteIngredientTypeCommandValidator : AbstractValidator<DeleteIngredientTypeCommand>
    {
        public DeleteIngredientTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
