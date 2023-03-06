using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Create
{
    public class CreateFoodSizeCommandValidator : AbstractValidator<CreateFoodSizeCommand>
    {
        public CreateFoodSizeCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.multiplier).NotEmpty().GreaterThanOrEqualTo(1.0);
            RuleFor(c => c.foodTypeId).NotEmpty().GreaterThan(0);
        }
    }
}
