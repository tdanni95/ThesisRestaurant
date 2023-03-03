using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.FoodSize
{
    public record CreateFoodSizeRequest(
            string Name,
            double Multiplier,
            int FoodTypeId
        );
}
