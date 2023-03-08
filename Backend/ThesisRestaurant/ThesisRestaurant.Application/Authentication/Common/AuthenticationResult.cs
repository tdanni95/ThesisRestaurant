using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Authentication.Common
{
    public record AuthenticationResult(
            User User,
            string Token
        );
}
