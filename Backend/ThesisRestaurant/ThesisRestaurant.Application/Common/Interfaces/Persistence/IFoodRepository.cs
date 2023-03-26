using ErrorOr;
using ThesisRestaurant.Domain.Foods;
using ThesisRestaurant.Domain.Foods.FoodPictures;
using ThesisRestaurant.Domain.Foods.FoodPrices;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IFoodRepository
    {
        Task<ErrorOr<Created>> CreateFood(Food food);
        Task<Food?> GetFoodById(int id);
        Task<List<Food>> GetAllFoods();
        Task<ErrorOr<Updated>> Update(Food value);
        Task<ErrorOr<Deleted>> Delete(int Id);

        Task<ErrorOr<Food>> AddDiscount(FoodPrice price, int foodId);
        Task<ErrorOr<Deleted>> DeleteDiscount(int id);

        Task<ErrorOr<Created>> UploadFile(int id, FoodPicture picture);
    }
}
