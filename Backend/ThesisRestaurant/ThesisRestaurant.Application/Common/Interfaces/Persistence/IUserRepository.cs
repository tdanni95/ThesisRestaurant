using ErrorOr;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<ErrorOr<User>> GetUserByEmail(string email);
        Task<ErrorOr<Created>> Add(User user);
        Task<ErrorOr<Updated>> Update(User user);
        Task<ErrorOr<Updated>> ChangePassword(string newPassword, int id);
        Task<ErrorOr<Updated>> ChangeLevel(byte newLevel, int id);
        Task<ErrorOr<Deleted>> Delete(int id);

        Task<ErrorOr<Created>> AddAddress(UserAddress address, int userId);
        Task<ErrorOr<Updated>> UpdateAddress(UserAddress address, int userId);
        Task<ErrorOr<Deleted>> DeleteAddress(UserAddress address, int userId);

        Task<ErrorOr<User>> GetUserById(int id);
    }
}
