using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Contracts.FoodType
{
    public record FoodTypeRequest(int Id, string Name, double Price);
}
