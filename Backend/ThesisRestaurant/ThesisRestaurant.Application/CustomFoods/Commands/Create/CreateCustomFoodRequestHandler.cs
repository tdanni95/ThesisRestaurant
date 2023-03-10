using ErrorOr;
using MediatR;
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

        public CreateCustomFoodRequestHandler(IIngredientRepository ingredientRepository, IFoodTypeRepository foodTypeRepository, ICustomFoodRepository customFoodRepository)
        {
            _ingredientRepository = ingredientRepository;
            _foodTypeRepository = foodTypeRepository;
            _customFoodRepository = customFoodRepository;
        }

        public async Task<ErrorOr<CustomFood>> Handle(CreateCustomFoodCommand request, CancellationToken cancellationToken)
        {
            var ingredients = await GetIngredients(request.IngredientIds);
            if (ingredients.IsError) return ingredients.Errors;


            var ingredientErros = ValidateIngredients(ingredients.Value);
            if (ingredientErros.Count > 0) return ingredientErros;

            var foodType = await _foodTypeRepository.GetById(request.FoodTypeId);
            if (foodType.IsError) return foodType.Errors;

            var customFood = CustomFood.Create(
                    request.Name,
                    CustomFood.CalculatePrice(foodType.Value, ingredients.Value),
                    ingredients.Value,
                    foodType.Value
                );
            var result = await _customFoodRepository.AddCustomFood(customFood);
            if (result.IsError) return result.Errors;

            return customFood;
        }

        private async Task<ErrorOr<List<Ingredient>>> GetIngredients(List<int> ids)
        {
            return await _ingredientRepository.GetWhereIdIn(ids);
        }

        private async Task<ErrorOr<FoodType>> GetFoodType(int foodTypeId)
        {
            return await _foodTypeRepository.GetById(foodTypeId);
        }


        private List<Error> ValidateIngredients(List<Ingredient> ingredients)
        {
            List<Error> errors = new();
            var typeByCount = BuildTypeDictionary(ingredients);
            foreach (var keyValuePair in typeByCount)
            {
                var type = ingredients.Select(i => i.IngredientType).Where(it => it.Id == keyValuePair.Key).First();

                int minOption = type.MinOption;
                int maxOption = type.MaxOption;

                if (keyValuePair.Value < minOption || keyValuePair.Value > maxOption)
                {
                    errors.Add(Errors.Ingredients.FoodCreatinCountError(type.Name, type.MinOption, type.MaxOption));
                }
            }
            return errors;
        }

        private Dictionary<int, int> BuildTypeDictionary(List<Ingredient> ingredients)
        {
            Dictionary<int, int> typeByCount = new();
            foreach (var ingredient in ingredients)
            {
                if (!typeByCount.ContainsKey(ingredient.IngredientType.Id))
                {
                    typeByCount.Add(ingredient.IngredientType.Id, 1);
                }
                else
                {
                    typeByCount[ingredient.IngredientType.Id] += 1;
                }
            }
            return typeByCount;
        }
    }
}

