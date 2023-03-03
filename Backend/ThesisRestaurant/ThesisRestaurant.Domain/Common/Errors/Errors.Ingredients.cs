using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Ingredients
        {
            public static Error NotFound => Error.NotFound(code: "Ingredient.NotFound", description: "Ingredient with given id does not exists");
            
        }
    }
}
