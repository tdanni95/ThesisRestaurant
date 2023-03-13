using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Foods.Common;

namespace ThesisRestaurant.Application.Foods.Queries.GetById
{
    public record GetFoodByIdQuery(int Id) : IRequest<ErrorOr<FoodResult>>;
}
