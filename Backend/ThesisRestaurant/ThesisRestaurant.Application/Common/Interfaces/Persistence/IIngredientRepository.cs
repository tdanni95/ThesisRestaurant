using ErrorOr;
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
        Task<ErrorOr<Ingredient>> GetById(int id);
        Task<ErrorOr<List<Ingredient>>> GetAll();
        Task<ErrorOr<List<Ingredient>>> GetAllByIngredientType(int ingredientTypeId);
        Task<ErrorOr<List<Ingredient>>> GetWhereIdIn(List<int> ids);

        Task<ErrorOr<Created>> Add(Ingredient ingredient);
        Task<ErrorOr<Updated>> Update(Ingredient ingredient);
        Task<ErrorOr<Deleted>> Delete(int id);

    }
}
