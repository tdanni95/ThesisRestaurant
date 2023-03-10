using ErrorOr;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Infrastructure.Services
{
    public class FoodIngredientValidator : IFoodIngredientValidator
    {
        public List<Error> ValidateIngredients(List<Ingredient> ingredients)
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
                int id = ingredient.IngredientType.Id;
                if (!typeByCount.ContainsKey(id))
                {
                    typeByCount.Add(id, 1);
                }
                else
                {
                    typeByCount[id] += 1;
                }
            }
            return typeByCount;
        }

    }
}
