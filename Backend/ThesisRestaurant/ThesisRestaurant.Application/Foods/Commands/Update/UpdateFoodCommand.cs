using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Foods.Common;

namespace ThesisRestaurant.Application.Foods.Commands.Update
{
    public record UpdateFoodCommand(int Id, string Name, double BasePrice, int FoodTypeId, List<int> IngredientIds) : IRequest<ErrorOr<FoodResult>>
    {
    }
}
