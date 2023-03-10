using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.FoodTypes;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.CustomFoods.Commands.Create
{
    public class CreateCustomFoodRequestHandler : IRequestHandler<CreateCustomFoodCommand, ErrorOr<CustomFood>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ICustomFoodRepository _customFoodRepository;
        private readonly IFoodIngredientValidator _foodIngredientValidation;

        public CreateCustomFoodRequestHandler(
            IIngredientRepository ingredientRepository, 
            IFoodTypeRepository foodTypeRepository, 
            ICustomFoodRepository customFoodRepository,
            IFoodIngredientValidator foodIngredientValidation)
        {
            _ingredientRepository = ingredientRepository;
            _foodTypeRepository = foodTypeRepository;
            _customFoodRepository = customFoodRepository;
            _foodIngredientValidation = foodIngredientValidation;
        }

        public async Task<ErrorOr<CustomFood>> Handle(CreateCustomFoodCommand request, CancellationToken cancellationToken)
        {
            var ingredients = await GetIngredients(request.IngredientIds);
            if (ingredients.IsError) return ingredients.Errors;


            var ingredientErros = _foodIngredientValidation.ValidateIngredients(ingredients.Value);
            if (ingredientErros.Count > 0) return ingredientErros;

            var foodType = await _foodTypeRepository.GetById(request.FoodTypeId);
            if (foodType.IsError) return foodType.Errors;

            var customFood = CustomFood.Create(
                    request.Name,
                    CustomFood.CalculatePrice(foodType.Value, ingredients.Value),
                    ingredients.Value,
                    foodType.Value
                );
            var result = await _customFoodRepository.AddCustomFood(customFood, request.UserId);
            if (result.IsError) return result.Errors;

            return customFood;
        }

        private async Task<ErrorOr<List<Ingredient>>> GetIngredients(List<int> ids)
        {
            return await _ingredientRepository.GetWhereIdIn(ids);
        }

    }
}

