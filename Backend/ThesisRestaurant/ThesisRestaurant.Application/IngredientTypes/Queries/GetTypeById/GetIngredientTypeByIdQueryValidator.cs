using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.IngredientTypes.Queries.GetTypeById
{
    public class GetIngredientTypeByIdQueryValidator : AbstractValidator<GetIngredientTypeByIdQuery>
    {
        public GetIngredientTypeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
