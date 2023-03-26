using ErrorOr;
using MediatR;

namespace ThesisRestaurant.Application.Foods.Commands.DeleteDiscount
{
    public record DeleteDiscountCommad(int id) : IRequest<ErrorOr<Deleted>>;
}
