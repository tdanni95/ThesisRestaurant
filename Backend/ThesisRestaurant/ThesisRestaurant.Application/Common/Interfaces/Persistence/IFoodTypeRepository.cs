using ErrorOr;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IFoodTypeRepository
    {
        Task<ErrorOr<FoodType>> GetById(int id);
        Task<ErrorOr<List<FoodType>>> GetAll();
        Task<ErrorOr<List<FoodType>>> GetAllByIngredientType(int ingredientTypeId);

        Task<ErrorOr<Created>> Add(FoodType ingredient);
        Task<ErrorOr<Updated>> Update(FoodType ingredient);
        Task<ErrorOr<Deleted>> Delete(int id);
    }
}
