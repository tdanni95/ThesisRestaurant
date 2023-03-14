using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ThesisRestaurant.Application.Common.Services;

namespace ThesisRestaurant.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                var givenName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
                var surName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname);
                result = $"{surName} {givenName}";
            }

            return result;
        }
    }
}
