using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.RefreshTokens;

namespace ThesisRestaurant.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);

        RefreshToken GenerateRefreshToken();
    }
}
