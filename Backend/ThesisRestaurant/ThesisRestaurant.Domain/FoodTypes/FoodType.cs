using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Domain.FoodTypes
{
    public class FoodType
    {
        public int Id { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }

        public List<FoodSize> FoodSizes { get; private set; }
    }
}
