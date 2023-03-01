using MediatR;
using ErrorOr;
using ThesisRestaurant.Application.Authentication.Common;

namespace ThesisRestaurant.Application.Authentication.Queries.Login
{
    public record LoginQuery (
            string Email,
            string Password
        ) : IRequest<ErrorOr<AuthenticationResult>>;
}
