using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class FoodSizeRepository : IFoodSizeRepository
    {
        private readonly ThesisRestaurantDbContext _context;

        public FoodSizeRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> Add(FoodSize foodSize)
        {
            _context.FoodSizes.Add(foodSize);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> Delete(int id)
        {
            var foodSize = await GetById(id);
            if (foodSize.IsError) return foodSize.Errors;
            _context.FoodSizes.Remove(foodSize.Value);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<List<FoodSize>>> GetAll()
        {
            return await _context.FoodSizes.Include(f => f.FoodType).ToListAsync();
        }


        public async Task<ErrorOr<FoodSize>> GetById(int id)
        {
            var foodSize = await _context.FoodSizes.Where(f => f.Id == id).Include(f => f.FoodType).FirstOrDefaultAsync();
            if(foodSize == null)
            {
                return Errors.FoodSizes.NotFound;
            }
            return foodSize;
        }

        public async Task<ErrorOr<Updated>> Update(FoodSize foodSize)
        {
            var dbFoodSize = await GetById(foodSize.Id);
            if (dbFoodSize.IsError) return dbFoodSize.Errors;
            dbFoodSize.Value.Update(foodSize);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
    }
}
