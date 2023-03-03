using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Contracts.IngredientTypes;

namespace ThesisRestaurant.Contracts.Ingredient
{
    public record UpdateIngredientRequest(
            int Id, string Name, double Price, int IngredientTypeId
        );
}
