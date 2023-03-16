using ErrorOr;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public OrderRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> PlaceOrder(Order order, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null) return Errors.Users.NotFound;
            user.Orders.Add(order);


            await _context.SaveChangesAsync();
            return Result.Created;
        }
    }
}
