using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class CustomFoodRepository : ICustomFoodRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public CustomFoodRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<ErrorOr<Created>> AddCustomFood(CustomFood customFood, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null) return Errors.User.NotFound;
            user.CustomFoods.Add(customFood);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<CustomFood?> GetById(int id)
        {
            return await _context.CustomFoods.FindAsync(id);
        }

        public async Task<List<CustomFood>> GetUserCustomFoods(int userId)
        {
            var list = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.CustomFoods)
                .Select(u => u.CustomFoods)
                .FirstOrDefaultAsync();
            return list!;
        }

        public async Task<ErrorOr<Updated>> UpdateCustomFood(CustomFood customFood, int userId)
        {
            var dbCf = await GetById(customFood.Id);
            if (dbCf is null) return Errors.CustomFoods.NotFound;
            //TODO owner or admin check
            dbCf.Update(customFood);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
    }
}
