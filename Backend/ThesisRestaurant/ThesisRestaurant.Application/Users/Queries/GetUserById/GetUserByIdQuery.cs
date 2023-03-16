using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<ErrorOr<User>>;
}
