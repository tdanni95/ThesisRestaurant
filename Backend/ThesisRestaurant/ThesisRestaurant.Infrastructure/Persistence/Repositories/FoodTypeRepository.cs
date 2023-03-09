using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.FoodTypes;
using static ThesisRestaurant.Domain.Common.Errors.Errors;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class FoodTypeRepository : IFoodTypeRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public FoodTypeRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> Add(FoodType foodType)
        {
            bool isTaken = await IsNameTaken(foodType.Name);
            if (isTaken) return Errors.FoodTypes.NameTaken;
            _context.FoodTypes.Add(foodType);
            await _context.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> Delete(int id)
        {
            var dbType = await GetById(id);
            if (dbType.IsError) return dbType.Errors;

            _context.FoodTypes.Remove(dbType.Value);
            await _context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<List<FoodType>>> GetAll()
        {
            return await _context.FoodTypes.ToListAsync();
        }

        public async Task<ErrorOr<FoodType>> GetById(int id)
        {
            var type = await _context.FoodTypes.FindAsync(id);
            if(type is null) return Errors.FoodTypes.NotFound;

            return type;
        }

        public async Task<ErrorOr<Updated>> Update(FoodType foodType)
        {
            var dbType = await GetById(foodType.Id);
            if (dbType.IsError) return dbType.Errors;

            bool isTaken = await IsNameTaken(foodType.Name, foodType.Id);
            if (isTaken) return Errors.FoodTypes.NameTaken;

            dbType.Value.Update(foodType);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }
        private async Task<bool> IsNameTaken(string name)
        {
            var type = await _context.FoodTypes.Where(s => s.Name == name).FirstOrDefaultAsync();
            return type is not null;
        }
        private async Task<bool> IsNameTaken(string name, int id)
        {
            var type = await _context.FoodTypes.Where(s => s.Name == name && s.Id != id).FirstOrDefaultAsync();
            return type is not null;
        }
    }
}
