using ErrorOr;
using ThesisRestaurant.Domain.CustomFoods;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        //TODO add foods as well
        public Task<ErrorOr<Created>> PlaceOrder(List<CustomFood> customFoodList);
    }
}
