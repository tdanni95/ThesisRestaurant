using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodTypes.Commands.Update
{
    public class UpdateFoodTypeCommandValidator : AbstractValidator<UpdateFoodTypeCommand>
    {
        public UpdateFoodTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
