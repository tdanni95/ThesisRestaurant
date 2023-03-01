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
    }
}
