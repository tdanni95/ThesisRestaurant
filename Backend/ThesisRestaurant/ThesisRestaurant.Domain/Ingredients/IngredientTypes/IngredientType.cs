using System.ComponentModel.DataAnnotations;

namespace ThesisRestaurant.Domain.Ingredients.IngredientTypes
{
    public class IngredientType
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
