using System.ComponentModel.DataAnnotations;

namespace ThesisRestaurant.Domain.FoodTypes.FoodSizes
{
    public class FoodSize
    {
        public int Id { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }
        public double Multiplier { get; private set; } = 1.0;
        public FoodType FoodType { get; private set; }

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

        public void Update(FoodSize foodSize)
        {
            Name = foodSize.Name;
            Multiplier = foodSize.Multiplier;
            FoodType = foodSize.FoodType;
        }

        public static FoodSize Create(string name, double multiplier, FoodType foodType, int id = 0)
        {
            return new(name, multiplier, foodType, id);
        }
    }
}
