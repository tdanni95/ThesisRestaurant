using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Delete
{
    public class DeleteFoodTypeCommandValidator : AbstractValidator<DeleteFoodTypeCommand>
    {
        public DeleteFoodTypeCommandValidator()
        {
            RuleFor(f => f.Id).NotEmpty().GreaterThan(0);
        }
    }
}
