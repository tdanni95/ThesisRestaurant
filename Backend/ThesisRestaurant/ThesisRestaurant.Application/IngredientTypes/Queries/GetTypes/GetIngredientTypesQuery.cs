using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Queries.GetTypes
{
    public record GetIngredientTypesQuery : IRequest<ErrorOr<List<IngredientType>>>;
}
