using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public UserRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> Add(User user)
        {
            var isEmailTaken = await GetUserByEmail(user.Email);
            if (isEmailTaken.IsError)
            {
                return isEmailTaken.Errors;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<User>> GetUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user is null)
            {
                return user;
            }
            return Errors.User.DuplicateEmail;
        }

        public async Task<ErrorOr<User>> GetUserById(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user is null)
            {
                return Errors.User.NotFound;
            }
            return user;
        }
    }
}
