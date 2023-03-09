using System.ComponentModel.DataAnnotations;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Domain.FoodTypes
{
    public class FoodType
    {
        public int Id { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }

        public double Price { get; private set; }

        public List<FoodSize> FoodSizes { get; private set; } = new();

        private FoodType(string name, double price, int id = 0)
        {
            Name = name;
            Price = price;
            Id = id;
        }

        public static FoodType Create(string name, double price, int id = 0)
        {
            return new(name, price, id);
        }

        public void Update(FoodType foodType)
        {
            this.Name = foodType.Name;
            this.Price = foodType.Price;
        }
    }
}
