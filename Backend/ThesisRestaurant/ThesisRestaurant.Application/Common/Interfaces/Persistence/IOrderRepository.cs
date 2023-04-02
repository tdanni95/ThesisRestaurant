using ErrorOr;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Users;

namespace ThesisRestaurant.Application.Common.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        //TODO add foods as well
        public Task<ErrorOr<Created>> PlaceOrder(Order order, int userId);

        public Task<ErrorOr<User>> GetUserOrders(int userId);
        public Task<ErrorOr<List<User>>> GetAllOrders();
    }
}
