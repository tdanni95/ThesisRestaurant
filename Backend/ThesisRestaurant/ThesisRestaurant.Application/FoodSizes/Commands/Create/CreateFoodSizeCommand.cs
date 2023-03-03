using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.FoodSizes.Commands.Create
{
    public record CreateFoodSizeCommand(string Name, double multiplier, int foodTypeId) : IRequest<ErrorOr<FoodSize>>;
}
