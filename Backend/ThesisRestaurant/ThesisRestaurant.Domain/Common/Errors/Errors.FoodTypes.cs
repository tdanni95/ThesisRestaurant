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
        public static class FoodTypes
        {
            public static Error NotFound => Error.NotFound(code: "FoodType.NotFound", description: "FoodType type with given id does not exists");
            public static Error NameTaken => Error.Conflict(code: "FoodType.NameTaken", description: "FoodType type with given name already exists");
        }
    }
}
