using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Delete
{
    public class DeleteFoodSizeCommandValidator : AbstractValidator<DeleteFoodSizeCommand>
    {
        public DeleteFoodSizeCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
        }
    }
}
