using FluentValidation;

namespace ThesisRestaurant.Application.Authentication.Queries.RefreshToken
{
    public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
    {
        public RefreshTokenQueryValidator()
        {
            RuleFor(x => x.refreshToken).NotEmpty();
            RuleFor(x => x.refreshToken).Must(ValidateRefreshToken).WithMessage("Invalid token");
        }

        private bool ValidateRefreshToken(string refreshToken)
        {
            return Guid.TryParse(refreshToken, out var refreshTokenId);
        }
    }
}
