using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.UserAddresses;

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
            if (user is null)
            {
                return user;
            }
            return Errors.User.DuplicateEmail;
        }
        public async Task<ErrorOr<User>> GetUserByEmail(string email, int id)
        {
            var user = await _context.Users.Where(u => u.Email == email && u.Id != id).FirstOrDefaultAsync();
            if (user is null)
            {
                return user;
            }
            return Errors.User.DuplicateEmail;
        }

        public async Task<ErrorOr<User>> GetUserById(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id).Include(u => u.UserAddresses).FirstOrDefaultAsync();
            if (user is null)
            {
                return Errors.User.NotFound;
            }
            return user;
        }

        public async Task<ErrorOr<Updated>> Update(User user)
        {
            var dbUser = await GetUserById(user.Id);
            if (dbUser.IsError) return dbUser.Errors;

            var isEmailTaken = await GetUserByEmail(user.Email, user.Id);
            if (isEmailTaken.IsError) return isEmailTaken.Errors;

            dbUser.Value.UpdateBaseFields(user.FirstName, user.LastName, user.Email, user.PhoneNumber);

            await _context.SaveChangesAsync();
            return Result.Updated;
        }



        public async Task<ErrorOr<Updated>> ChangeLevel(byte newLevel, int id)
        {
            var dbUser = await GetUserById(id);
            if (dbUser.IsError) return dbUser.Errors;
            dbUser.Value.SetLevel(newLevel);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> ChangePassword(string newPassword, int id)
        {
            var dbUser = await GetUserById(id);
            if (dbUser.IsError) return dbUser.Errors;
            dbUser.Value.SetPassword(newPassword);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Deleted>> Delete(int id)
        {
            var dbUser = await GetUserById(id);
            if (dbUser.IsError) return dbUser.Errors;
            _context.Users.Remove(dbUser.Value);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<Created>> AddAddress(UserAddress address, int userId)
        {
            var dbUser = await GetUserById(userId);
            if (dbUser.IsError) return dbUser.Errors;
            dbUser.Value.UserAddresses.Add(address);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Updated>> UpdateAddress(UserAddress address, int userId)
        {
            var dbUser = await GetUserById(userId);
            if (dbUser.IsError) return dbUser.Errors;
            var dbAddress = dbUser.Value.UserAddresses.Where(a => a.Id == address.Id).FirstOrDefault();
            if (dbAddress is null)
            {
                return Errors.User.AddressNotFound;
            }
            dbAddress.Update(address);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
        public async Task<ErrorOr<Deleted>> DeleteAddress(UserAddress address, int userId)
        {
            var dbUser = await GetUserById(userId);
            if (dbUser.IsError) return dbUser.Errors;
            var dbAddress = dbUser.Value.UserAddresses.Where(a => a.Id == address.Id).FirstOrDefault();
            if (dbAddress is null)
            {
                return Errors.User.AddressNotFound;
            }
            _context.UserAddresses.Remove(address);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }
    }
}
