using ErrorOr;
using MediatR;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Users.Commands.Update
{
    public record UpdateUserCommand(int Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber) : IRequest<ErrorOr<User>>;
}
