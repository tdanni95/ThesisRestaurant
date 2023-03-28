using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<ErrorOr<List<User>>>;
}
