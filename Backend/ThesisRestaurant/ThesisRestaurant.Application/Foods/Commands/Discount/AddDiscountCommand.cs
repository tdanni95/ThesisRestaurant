using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Foods.Common;

namespace ThesisRestaurant.Application.Foods.Commands.Discount
{
    public record AddDiscountCommand(int FoodId, double Price, string From, string To) : IRequest<ErrorOr<FoodResult>>
    {
    }
}
