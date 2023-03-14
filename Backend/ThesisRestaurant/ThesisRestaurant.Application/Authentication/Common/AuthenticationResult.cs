using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.RefreshTokens;

namespace ThesisRestaurant.Application.Authentication.Common
{
    public record AuthenticationResult(
            User User,
            string Token,
            RefreshToken RefreshToken
        );
}
