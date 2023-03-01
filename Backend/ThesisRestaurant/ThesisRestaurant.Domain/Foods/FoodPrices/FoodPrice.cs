using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Domain.Foods.FoodPrices
{
    public class FoodPrice
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}

