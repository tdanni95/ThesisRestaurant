using ErrorOr;
using ThesisRestaurant.Domain.FoodTypes.FoodSizes;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IFoodSizeRepository
    {
        Task<ErrorOr<FoodSize>> GetById(int id);

        Task<ErrorOr<List<FoodSize>>> GetAll();

        Task<ErrorOr<Created>> Add(FoodSize foodSize);
        Task<ErrorOr<Updated>> Update(FoodSize foodSize);
        Task<ErrorOr<Deleted>> Delete(int id);
    }
}
