using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.FoodSizes.Queries.GetById
{
    public record GetFoodSizeByIdQuery(int Id) : IRequest<ErrorOr<FoodSize>>;
}
