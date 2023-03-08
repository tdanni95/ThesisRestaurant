using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Contracts.FoodType;

namespace ThesisRestaurant.Contracts.FoodSize
{
    public record FoodSizeResponse(int Id, string Name, double Multiplier, FoodTypeResponse FoodType);
}
