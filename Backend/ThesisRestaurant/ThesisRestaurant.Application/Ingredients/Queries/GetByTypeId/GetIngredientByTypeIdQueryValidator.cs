using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetByTypeId
{
    public class GetIngredientByTypeIdQueryValidator : AbstractValidator<GetIngredientByTypeIdQuery>
    {
        public GetIngredientByTypeIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
