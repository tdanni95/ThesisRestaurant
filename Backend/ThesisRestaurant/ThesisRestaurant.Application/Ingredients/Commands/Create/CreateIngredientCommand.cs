using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.IngredientTypes.Common;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Ingredients.Commands.Create
{
    public record CreateIngredientCommand(
            string Name, double Price, int IngredientTypeId
        ) : IRequest<ErrorOr<Ingredient>>;


}
