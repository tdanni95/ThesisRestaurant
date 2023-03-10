using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.CustomFoods.Queries.GetMyCustomFoods
{
    public record GetMyCustomFoodsQuery(int UserId) : IRequest<ErrorOr<List<CustomFood>>>;
}
