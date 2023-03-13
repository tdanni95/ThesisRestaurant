using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public FoodRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> CreateFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<List<Food>> GetAllFoods()
        {
            return await _context.Foods
                .Where(f => f.Visible == 1)
                .Include(f => f.FoodPictures)
                .Include(f => f.Type)
                .Include(f => f.FoodPrices)
                .Include(f => f.Ingredients)
                .ThenInclude(i => i.IngredientType)
                .ToListAsync();
        }

        public async Task<Food?> GetFoodById(int id)
        {
            return await _context.Foods.Where(f => f.Id == id && f.Visible == 1)
                .Include(f => f.FoodPictures)
                .Include(f => f.Type)
                .Include(f => f.FoodPrices)
                .Include(f => f.Ingredients)
                .ThenInclude(i => i.IngredientType)
                .FirstOrDefaultAsync();
        }
    }
}
