using ThesisRestaurant.Domain.Entities;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);
    }
}
