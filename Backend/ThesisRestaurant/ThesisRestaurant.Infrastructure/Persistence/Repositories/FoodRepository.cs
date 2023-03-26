using ErrorOr;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Foods.FoodPictures;
using ThesisRestaurant.Domain.Foods.FoodPrices;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public FoodRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Food>> AddDiscount(FoodPrice price, int foodId)
        {
            if (price.From > price.To) return Errors.Foods.InvalidDiscountDates;
            var dbFood = await GetFoodById(foodId);
            if (dbFood is null) return Errors.Foods.NotFound;

            if (price.Price > dbFood.BasePrice) return Errors.Foods.InvalidDiscountValue;

            dbFood.AddDiscount(price);
            await _context.SaveChangesAsync();

            return dbFood;
        }

        public async Task<ErrorOr<Created>> CreateFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> Delete(int Id)
        {
            var dbFood = await GetFoodById(Id);
            if (dbFood is null) return Errors.Foods.NotFound;

            dbFood.Delete();
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<Deleted>> DeleteDiscount(int id)
        {
            var discount = await  _context.FoodPrices.Where(fp => fp.Id == id).FirstOrDefaultAsync();
            if (discount is null) return Errors.Foods.NotFound;
            _context.FoodPrices.Remove(discount);
            await _context.SaveChangesAsync();
            return Result.Deleted;
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

        public async Task<ErrorOr<Updated>> Update(Food value)
        {
            var dbFood = await GetFoodById(value.Id);
            if (dbFood is null) return Errors.Foods.NotFound;

            dbFood.Update(value);
            await _context.SaveChangesAsync();

            return Result.Updated;
        }

        public async Task<ErrorOr<Created>> UploadFile(int id, FoodPicture picture)
        {
            var dbFood = await GetFoodById(id);
            if (dbFood is null) return Errors.Foods.NotFound;
            dbFood.FoodPictures.Add(picture);
            await _context.SaveChangesAsync();
            return Result.Created;
        }
    }
}
