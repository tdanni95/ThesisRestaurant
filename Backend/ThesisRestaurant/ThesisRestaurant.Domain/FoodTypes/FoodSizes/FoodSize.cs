using System.ComponentModel.DataAnnotations;

namespace ThesisRestaurant.Domain.FoodTypes.FoodSizes
{
    public class FoodSize
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public double Multiplier { get; set; } = 1.0;
        public FoodType FoodType { get; set; }

        private FoodSize(string name, double multiplier, int id = 0)
        {
            Id = id;
            Name = name;
            Multiplier = multiplier;
        }

        private FoodSize(string name, double multiplier, FoodType foodType, int id = 0)
        {
            Id = id;
            Name = name;
            Multiplier = multiplier;
            FoodType = foodType;
        }

        public void Update(string name, double multiplier, FoodType foodType)
        {
            Name = name;
            Multiplier = multiplier;
            FoodType = foodType;
        }

        public static FoodSize Create(string name, double multiplier, FoodType foodType)
        {
            return new(name, multiplier, foodType);
        }
    }
}
