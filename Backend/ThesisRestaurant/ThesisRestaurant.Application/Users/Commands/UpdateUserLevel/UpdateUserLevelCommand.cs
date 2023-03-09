using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Commands.UpdateUserLevel
{
    public record UpdateUserLevelCommand(int Id, byte Level) : IRequest<ErrorOr<Updated>>;
}
