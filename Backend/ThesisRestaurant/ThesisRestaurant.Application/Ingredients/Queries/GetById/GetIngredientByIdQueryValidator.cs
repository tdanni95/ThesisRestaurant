using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetById
{
    public class GetIngredientByIdQueryValidator : AbstractValidator<GetIngredientByIdQuery>
    {
        public GetIngredientByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
