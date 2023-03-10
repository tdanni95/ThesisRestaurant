using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public IngredientRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> Add(Ingredient ingredient)
        {
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            return Result.Created;
        }



        public async Task<ErrorOr<List<Ingredient>>> GetAll()
        {
            return await _context.Ingredients
                .Include(i => i.IngredientType)
                .ToListAsync();
        }

        public async Task<ErrorOr<List<Ingredient>>> GetAllByIngredientType(int ingredientTypeId)
        {
            return await _context.Ingredients
                .Where(i => i.IngredientType.Id == ingredientTypeId)
                .Include(i => i.IngredientType)
                .ToListAsync();
        }

        public async Task<ErrorOr<Ingredient>> GetById(int id)
        {
            var ingredient = await _context.Ingredients
                .Where(i => i.Id == id)
                .Include(i => i.IngredientType)
                .FirstOrDefaultAsync();
            if (ingredient is null) return Errors.Ingredients.NotFound;
            return ingredient;
        }

        public async Task<ErrorOr<Updated>> Update(Ingredient ingredient)
        {
            var dbIngredient = await GetById(ingredient.Id);
            if (dbIngredient.IsError) return dbIngredient.Errors;
            dbIngredient.Value.Update(ingredient);
            await _context.SaveChangesAsync();
            return Result.Updated;
        }

        public async Task<ErrorOr<Deleted>> Delete(int id)
        {
            var ingredient = await GetById(id);
            if (ingredient.IsError) return ingredient.Errors;
            _context.Ingredients.Remove(ingredient.Value);
            await _context.SaveChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<List<Ingredient>>> GetWhereIdIn(List<int> ids)
        {
            var ingredients = await _context.Ingredients.Where(x => ids.Contains(x.Id)).Include(x => x.IngredientType).ToListAsync();

            if (ingredients.Count < ids.Distinct().ToList().Count) return Errors.Ingredients.NotFound;

            return ingredients;
        }

    }
}
