using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.Ingredients.Common;

namespace ThesisRestaurant.Application.IngredientTypes.Common
{
    public record IngredientTypeResult(int Id, string Name);
    public record IngredientTypeIngredientsResult(int Id, string Name, List<IngredientResult> Ingredients);
}
