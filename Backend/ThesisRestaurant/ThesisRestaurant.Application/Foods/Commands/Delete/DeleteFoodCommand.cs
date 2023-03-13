using ErrorOr;
using MediatR;

namespace ThesisRestaurant.Application.Foods.Commands.Delete
{
    public record DeleteFoodCommand(int id) : IRequest<ErrorOr<Deleted>>;
}
