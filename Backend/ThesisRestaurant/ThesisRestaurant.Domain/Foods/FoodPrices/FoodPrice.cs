using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Domain.Foods.FoodPrices
{
    public class FoodPrice
    {
        public int Id { get; private set; }
        public double Price { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        private FoodPrice(int id, double price, DateTime from, DateTime to)
        {
            Id = id;
            Price = price;
            From = from;
            To = to;
        }

        public static FoodPrice Create(double price, DateTime from, DateTime to, int id = 0)
        {
            return new(id, price , from, to);
        }
    }
}

