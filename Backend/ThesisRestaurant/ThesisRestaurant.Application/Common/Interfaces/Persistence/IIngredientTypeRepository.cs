using ErrorOr;
using ThesisRestaurant.Domain.Ingredients.IngredientTypes;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IIngredientTypeRepository
    {
        Task<ErrorOr<IngredientType>> GetById(int id);
        Task<ErrorOr<List<IngredientType>>> GetAll();

        Task<ErrorOr<Created>> Add(IngredientType ingredient);
        Task<ErrorOr<IngredientType>> Update(IngredientType ingredient);
        Task<ErrorOr<Deleted>> Delete(int id);
    }
}
