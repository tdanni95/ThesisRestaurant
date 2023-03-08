using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
