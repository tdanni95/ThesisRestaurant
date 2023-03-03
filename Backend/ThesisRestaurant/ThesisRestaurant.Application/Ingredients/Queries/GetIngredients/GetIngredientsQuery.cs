using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Queries.GetIngredients
{
    public record GetIngredientsQuery : IRequest<ErrorOr<List<Ingredient>>>;
    
}
