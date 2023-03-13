using ErrorOr;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Infrastructure.Services
{
    public class FoodBuilder : IFoodBuilder
    {

        private readonly IIngredientRepository _ingredientRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly IFoodIngredientValidator _foodIngredientValidator;

        public FoodBuilder(IIngredientRepository ingredientRepository, IFoodTypeRepository foodTypeRepository, IFoodIngredientValidator foodIngredientValidator)
        {
            _ingredientRepository = ingredientRepository;
            _foodTypeRepository = foodTypeRepository;
            _foodIngredientValidator = foodIngredientValidator;
        }

        public async Task<ErrorOr<Food>> Create(string name, double basePrice, int foodTypeId, List<int> ingredientIds, int id = 0)
        {
            var ingredients = await GetIngredients(ingredientIds);
            if (ingredients.IsError) return ingredients.Errors;

            var ingredientErros = _foodIngredientValidator.ValidateIngredients(ingredients.Value);
            if (ingredientErros.Count > 0) return ingredientErros;

            var foodType = await GetFoodTypeById(foodTypeId);
            if (foodType.IsError) return foodType.Errors;

            return Food.Create(name, foodType.Value, basePrice, ingredients.Value, id);
        }

        private async Task<ErrorOr<FoodType>> GetFoodTypeById(int id)
        {
            return await _foodTypeRepository.GetById(id);
        }

        private async Task<ErrorOr<List<Ingredient>>> GetIngredients(List<int> ids)
        {
            return await _ingredientRepository.GetWhereIdIn(ids);
        }
    }
}
