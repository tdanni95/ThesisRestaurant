using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.IngredientTypes
{
    public record UpdateIngredientTypeRequest(int Id, string Name);
}
