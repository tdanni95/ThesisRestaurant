using ErrorOr;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<ErrorOr<User>> GetUserByEmail(string email);
        Task<ErrorOr<Created>> Add(User user);

        Task<ErrorOr<User>> GetUserById(int id);
    }
}
