using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Users;
using ThesisRestaurant.Domain.Users.RefreshTokens;
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

        public async Task<ErrorOr<List<User>>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(u => u.UserAddresses)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .ToListAsync();
            return users;
        }

        public async Task<ErrorOr<Created>> Add(User user)
        {
            var isEmailTaken = await GetUserByEmail(user.Email);
            if (isEmailTaken is not null)
            {
                return Errors.Users.DuplicateEmail;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Updated>> Login(User user, RefreshToken? token)
        {
            user.SetAuthToken(token);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }


        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserByEmail(string email, int id)
        {
            return await _context.Users.Where(u => u.Email == email && u.Id != id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.Where(u => u.Id == id).Include(u => u.UserAddresses).FirstOrDefaultAsync();
        }

        public async Task<ErrorOr<Updated>> Update(User user)
        {
            var dbUser = await GetUserById(user.Id);
            if (dbUser is null) return Errors.Users.NotFound;

            var isEmailTaken = await GetUserByEmail(user.Email, user.Id);
            if (isEmailTaken is not null) return Errors.Users.DuplicateEmail;

            dbUser!.UpdateBaseFields(user.FirstName, user.LastName, user.Email, user.PhoneNumber);

            await _context.SaveChangesAsync();
            return Result.Updated;
        }



        public async Task<ErrorOr<Updated>> ChangeLevel(byte newLevel, int id)
        {
            var dbUser = await GetUserById(id);
            if (dbUser is null) return Errors.Users.NotFound;
            dbUser!.SetLevel(newLevel);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Updated>> ChangePassword(string newPassword, int id)
        {
            var dbUser = await GetUserById(id);
            if (dbUser is null) return Errors.Users.NotFound;
            dbUser!.SetPassword(newPassword);
            //force new login
            dbUser!.SetAuthToken(null);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Deleted>> Delete(int id)
        {
            var dbUser = await GetUserById(id);
            if (dbUser is not null) return Errors.Users.NotFound;
            _context.Users.Remove(dbUser!);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<Created>> AddAddress(UserAddress address, int userId)
        {
            var dbUser = await GetUserById(userId);
            if (dbUser is null) return Errors.Users.NotFound;
            dbUser!.UserAddresses.Add(address);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Updated>> UpdateAddress(UserAddress address, int userId)
        {
            var dbUser = await GetUserById(userId);
            if (dbUser is null) return Errors.Users.NotFound;
            var dbAddress = dbUser!.UserAddresses.Where(a => a.Id == address.Id).FirstOrDefault();
            if (dbAddress is null)
            {
                return Errors.Users.AddressNotFound;
            }
            dbAddress.Update(address);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
        public async Task<ErrorOr<Deleted>> DeleteAddress(int addressId, int userId)
        {
            var dbUser = await GetUserById(userId);
            if (dbUser is null) return Errors.Users.NotFound;
            var dbAddress = dbUser!.UserAddresses.Where(a => a.Id == addressId).FirstOrDefault();
            if (dbAddress is null)
            {
                return Errors.Users.AddressNotFound;
            }
            _context.UserAddresses.Remove(dbAddress);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<User?> GetuserByRefreshToken(string refreshToken)
        {
            return await _context.Users.Where(u => u.RefreshToken != null && u.RefreshToken.Token == refreshToken).FirstOrDefaultAsync();
        }


    }
}
