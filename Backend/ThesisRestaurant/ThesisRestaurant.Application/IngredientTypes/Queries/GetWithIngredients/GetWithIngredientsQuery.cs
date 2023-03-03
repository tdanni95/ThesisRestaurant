using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.IngredientTypes.Queries.GetWithIngredients
{
    public record GetWithIngredientsQuery : IRequest<ErrorOr<List<IngredientType>>>;    
}
