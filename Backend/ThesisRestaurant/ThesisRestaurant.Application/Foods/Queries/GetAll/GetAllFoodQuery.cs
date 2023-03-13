using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Foods.Common;

namespace ThesisRestaurant.Application.Foods.Queries.GetAll
{
    public record GetAllFoodQuery : IRequest<ErrorOr<List<FoodResult>>>;
}
