using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;

namespace ThesisRestaurant.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
          string FirstName,
          string LastName,
          string Email,
          string Password
      ) : IRequest<ErrorOr<AuthenticationResult>>;
}
