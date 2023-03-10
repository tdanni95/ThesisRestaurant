using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Create
{
    public record CreateCustomFoodCommand(int UserId, string Name, int FoodTypeId, List<int> IngredientIds) : IRequest<ErrorOr<CustomFood>>;
}
