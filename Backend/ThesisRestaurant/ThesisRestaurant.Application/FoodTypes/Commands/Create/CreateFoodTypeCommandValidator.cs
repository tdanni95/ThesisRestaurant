using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Create
{
    public class CreateFoodTypeCommandValidator : AbstractValidator<CreateFoodTypeCommand>
    {
        public CreateFoodTypeCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
