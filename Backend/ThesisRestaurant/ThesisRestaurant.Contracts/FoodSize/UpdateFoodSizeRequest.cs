using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.FoodSize
{
    public record UpdateFoodSizeRequest(int Id, string Name, double Multiplier, int FoodTypeId);
}
