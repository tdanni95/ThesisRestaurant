using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.FoodTypes.Queries.GetById
{
    public class GetFoodTypeByIdQueryValidator : AbstractValidator<GetFoodTypeByIdQuery>
    {
        public GetFoodTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
