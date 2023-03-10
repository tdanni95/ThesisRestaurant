using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;
using ThesisRestaurant.Infrastructure.Persistence;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class IngredientTypeRepository : IIngredientTypeRepository
    {
        private readonly ThesisRestaurantDbContext _dbContext;

        public IngredientTypeRepository(ThesisRestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Created>> Add(IngredientType ingredient)
        {
            bool isTaken = await IsNameTaken(ingredient.Name);
            if (isTaken) return Errors.IngredientTypes.NameTaken;
            await _dbContext.IngredientTypes.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> Delete(int id)
        {
            var type = await _dbContext.IngredientTypes.FindAsync(id);
            if (type is null)
            {
                return Errors.IngredientTypes.NotFound;
            }
            _dbContext.IngredientTypes.Remove(type);
            await _dbContext.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<List<IngredientType>>> GetAll()
        {
            var types = await _dbContext.IngredientTypes.Include(x => x.ingredients).ToListAsync();
            return types;
        }

        public async Task<ErrorOr<IngredientType>> GetById(int id)
        {
            var type = await _dbContext.IngredientTypes.FindAsync(id);
            if (type is null)
            {
                return Errors.IngredientTypes.NotFound;
            }
            return type;
        }

        private async Task<bool> IsNameTaken(string name)
        {
            var type = await _dbContext.IngredientTypes.Where(it => it.Name == name).FirstOrDefaultAsync();
            return type is not null;
        }

        public async Task<ErrorOr<Updated>> Update(IngredientType ingredient)
        {
            var result = await GetById(ingredient.Id);
            if (result.IsError) return result.Errors;

            bool isTaken = await IsNameTaken(ingredient.Name);
            if (isTaken) return Errors.IngredientTypes.NameTaken;

            var type = result.Value;
            type.Update(ingredient);
            await _dbContext.SaveChangesAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<List<IngredientType>>> GetAllWithIngredient()
        {
            var types = await _dbContext.IngredientTypes.Include(x => x.ingredients).ToListAsync();
            return types;
        }
    }
}
