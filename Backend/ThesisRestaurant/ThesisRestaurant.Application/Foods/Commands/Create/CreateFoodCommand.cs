using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Foods.Common;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Application.Foods.Commands.Create
{
    public record CreateFoodCommand(string Name, double BasePrice, int FoodTypeId, List<int> IngredientIds) : IRequest<ErrorOr<FoodResult>>;
}
