using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Update
{
    public record UpdateCustomFoodCommand(int Id, int UserId, string Name, int FoodTypeId, List<int> IngredientIds) : IRequest<ErrorOr<CustomFood>>;
}
