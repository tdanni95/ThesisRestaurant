using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Application.FoodTypes.Common;

namespace ThesisRestaurant.Application.FoodSizes.Common
{
    public record FoodSizeResult(int Id, string Name, double Multiplier, FoodTypeResult FoodType);
}
