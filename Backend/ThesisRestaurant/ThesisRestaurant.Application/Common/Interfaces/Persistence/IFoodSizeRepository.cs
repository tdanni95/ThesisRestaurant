using ErrorOr;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IFoodSizeRepository
    {
        Task<ErrorOr<FoodSize>> GetById(int id);

        Task<ErrorOr<List<FoodSize>>> GetAll();
        Task<ErrorOr<List<FoodSize>>> GetAllWithIngredient();

        Task<ErrorOr<Created>> Add(FoodSize ingredient);
        Task<ErrorOr<Updated>> Update(FoodSize ingredient);
        Task<ErrorOr<Deleted>> Delete(int id);
    }
}
