using ErrorOr;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.RefreshTokens;
using ThesisRestaurant.Domain.Users.UserAddresses;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetuserByRefreshToken(string refreshToken);
        Task<ErrorOr<Created>> Add(User user);
        Task<ErrorOr<Updated>> Login(User user, RefreshToken? token);
        Task<ErrorOr<Updated>> Update(User user);
        Task<ErrorOr<Updated>> ChangePassword(string newPassword, int id);
        Task<ErrorOr<Updated>> ChangeLevel(byte newLevel, int id);
        Task<ErrorOr<Deleted>> Delete(int id);

        Task<ErrorOr<Created>> AddAddress(UserAddress address, int userId);
        Task<ErrorOr<Updated>> UpdateAddress(UserAddress address, int userId);
        Task<ErrorOr<Deleted>> DeleteAddress(int addressId, int userId);

        Task<User?> GetUserById(int id);
    }
}
