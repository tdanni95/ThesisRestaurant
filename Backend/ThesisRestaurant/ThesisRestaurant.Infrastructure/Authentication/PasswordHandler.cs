using BCrypt.Net;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;

namespace ThesisRestaurant.Infrastructure.Authentication
{
    public class PasswordHandler : IPasswordHandler
    {
        public string HashPassword(string passWord)
        {
            string newPassword = BCrypt.Net.BCrypt.HashPassword(passWord);
            return newPassword;
        }

        public bool VerifyPassword(string givenPassword, string actualPassword)
        {
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(givenPassword, actualPassword);
            return isValidPassword;
        }
    }
}
