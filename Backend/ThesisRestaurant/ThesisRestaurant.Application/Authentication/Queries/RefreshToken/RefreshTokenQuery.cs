using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Authentication.Common;

namespace ThesisRestaurant.Application.Authentication.Queries.RefreshToken
{
    public record RefreshTokenQuery(string? refreshToken) : IRequest<ErrorOr<AuthenticationResult>>;
}
