using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Queries.GetById
{
    public record GetCustomFoodByIdQuery(int UserId, int CustomFoodId) : IRequest<ErrorOr<CustomFood>>;
}
