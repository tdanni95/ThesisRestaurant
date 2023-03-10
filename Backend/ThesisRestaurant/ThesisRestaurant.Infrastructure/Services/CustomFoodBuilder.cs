using ErrorOr;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Infrastructure.Services
{
    public class CustomFoodBuilder : ICustomFoodBuilder
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly IFoodIngredientValidator _foodIngredientValidator;
        public CustomFoodBuilder(IIngredientRepository ingredientRepository, IFoodTypeRepository foodTypeRepository, IFoodIngredientValidator foodIngredientValidator)
        {
            _ingredientRepository = ingredientRepository;
            _foodTypeRepository = foodTypeRepository;
            _foodIngredientValidator = foodIngredientValidator;
        }

        public async Task<ErrorOr<CustomFood>> Create(string name, int foodTypeId, List<int> ingredientIds, int id = 0)
        {
            var ingredients = await GetIngredients(ingredientIds);
            if (ingredients.IsError) return ingredients.Errors;
            
            var ingredientErros = _foodIngredientValidator.ValidateIngredients(ingredients.Value);
            if (ingredientErros.Count > 0) return ingredientErros;

            var foodType = await GetFoodTypeById(foodTypeId);
            if (foodType.IsError) return foodType.Errors;

            return CustomFood.Create(
                    name,
                    CustomFood.CalculatePrice(foodType.Value, ingredients.Value),
                    ingredients.Value,
                    foodType.Value,
                    id
                );
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
