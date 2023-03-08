using System.ComponentModel.DataAnnotations;

namespace ThesisRestaurant.Domain.Ingredients.IngredientTypes
{
    public class IngredientType
    {
        public int Id { get; private set; }
        public int MinOption { get; private set; }
        public int MaxOption { get; private set; }
        [MaxLength(255)]
        public string Name { get; private set; }

        public List<Ingredient> ingredients { get; private set; }

        private IngredientType(string Name, int MinOption, int MaxOption, int Id = 0)
        {
            this.Id = Id;
            this.MinOption = MinOption;
            this.MaxOption = MaxOption;
            this.Name = Name;
        }

        public static IngredientType Create(string Name, int MinOption, int MaxOption, int Id = 0)
        {
            return new(Name, MinOption, MaxOption, Id);
        }

        public void Update(IngredientType ingredient)
        {
            this.Name = ingredient.Name;
            this.MinOption = ingredient.MinOption;
            this.MaxOption = ingredient.MaxOption;
        }

    }
}
