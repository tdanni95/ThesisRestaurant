using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisRestaurant.Domain.Ingredients;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IIngredientRepository
    {
        Ingredient? GetById(int id);
        List<Ingredient> GetAll();
        List<Ingredient> GetAllByIngredientType(int ingredientTypeId);

        void Add(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(int id);

    }
}
