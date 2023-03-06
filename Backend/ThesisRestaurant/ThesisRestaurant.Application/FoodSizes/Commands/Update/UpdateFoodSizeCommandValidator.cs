using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Update
{
    public class UpdateFoodSizeCommandValidator : AbstractValidator<UpdateFoodSizeCommand>
    {
        public UpdateFoodSizeCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Multiplier).NotEmpty().GreaterThanOrEqualTo(1.0);
            RuleFor(c => c.FoodTypeId).NotEmpty().GreaterThan(0);
        }
    }
}
