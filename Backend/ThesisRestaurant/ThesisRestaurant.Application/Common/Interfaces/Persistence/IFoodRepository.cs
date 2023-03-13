using ErrorOr;
using ThesisRestaurant.Domain.Foods;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IFoodRepository
    {
        Task<ErrorOr<Created>> CreateFood(Food food);
        Task<Food?> GetFoodById(int id);
        Task<List<Food>> GetAllFoods();
    }
}
