using ErrorOr;
using ThesisRestaurant.Domain.Orders;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        //TODO add foods as well
        public Task<ErrorOr<Created>> PlaceOrder(Order order, int userId);
    }
}
