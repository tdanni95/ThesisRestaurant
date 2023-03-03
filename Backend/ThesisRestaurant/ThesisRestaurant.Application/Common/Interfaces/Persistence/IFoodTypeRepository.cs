using ErrorOr;
using ThesisRestaurant.Domain.FoodTypes;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IFoodTypeRepository
    {
        Task<ErrorOr<FoodType>> GetById(int id);
        Task<ErrorOr<List<FoodType>>> GetAll();

        Task<ErrorOr<Created>> Add(FoodType foodType);
        Task<ErrorOr<Updated>> Update(FoodType foodType);
        Task<ErrorOr<Deleted>> Delete(int id);
    }
}
