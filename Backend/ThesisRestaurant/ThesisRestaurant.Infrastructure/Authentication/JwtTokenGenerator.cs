using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly JwtSettings jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                       SecurityAlgorithms.HmacSha256);

            var role = UserRoles.Roles.GetValueOrDefault(user.Level);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, role ?? "AppUser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
