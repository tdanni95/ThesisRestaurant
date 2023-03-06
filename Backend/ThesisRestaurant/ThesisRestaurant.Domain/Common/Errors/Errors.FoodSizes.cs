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
        public static class FoodSizes
        {
            public static Error NotFound => Error.Conflict(code: "FoodSize.NotFound", description: "The Food Size with given id is not found");

            public static Error NameTaken => Error.Conflict(code: "FoodSize.NameTaken", description: "Food size type with given name already exists");
        }
    }
}
