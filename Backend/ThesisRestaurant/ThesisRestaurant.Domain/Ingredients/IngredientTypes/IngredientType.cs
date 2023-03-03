using System.ComponentModel.DataAnnotations;

namespace ThesisRestaurant.Domain.Ingredients.IngredientTypes
{
    public class IngredientType
    {
        public int Id { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }

        private IngredientType(string Name, int Id = 0)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public static IngredientType Create(string Name, int Id = 0)
        {
            return new(Name, Id);
        }

        public void Update(IngredientType ingredient)
        {
            this.Name= ingredient.Name;
        }

    }
}
