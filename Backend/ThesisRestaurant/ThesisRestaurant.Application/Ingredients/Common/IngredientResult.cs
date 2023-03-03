using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.IngredientTypes.Common;

namespace ThesisRestaurant.Application.Ingredients.Common
{
    public record IngredientResult(int Id, string Name, double Price, IngredientTypeResult IngredientTypeResult);
}
