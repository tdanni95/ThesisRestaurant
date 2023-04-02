using ErrorOr;
using Microsoft.EntityFrameworkCore;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Common.Errors;
using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Users;
using static ThesisRestaurant.Domain.Common.Errors.Errors;

namespace ThesisRestaurant.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ThesisRestaurantDbContext _context;
        public OrderRepository(ThesisRestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<User>>> GetAllOrders()
        {
            var result = await _context.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .ThenInclude(cf => cf.CustumFood.FoodType)
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .ThenInclude(cf => cf.CustumFood.Ingredients)
                .ThenInclude(i => i.IngredientType)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.FoodSize)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.Food.Ingredients)
                .ThenInclude(ig => ig.IngredientType)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.Food.Type)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.AdditionalIngredients)
                .ThenInclude(ai => ai.Ingredient)
                .ToListAsync();

            return result;
        }

        public async Task<ErrorOr<User>> GetUserOrders(int userId)
        {
            var result = await _context.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .ThenInclude(cf => cf.CustumFood.FoodType)
                .Include(u => u.Orders)
                .ThenInclude(o => o.CustomFoods)
                .ThenInclude(cf => cf.CustumFood.Ingredients)
                .ThenInclude(i => i.IngredientType)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.FoodSize)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.Food.Ingredients)
                .ThenInclude(ig => ig.IngredientType)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.Food.Type)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Foods)
                .ThenInclude(o => o.AdditionalIngredients)
                .ThenInclude(ai => ai.Ingredient)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (result is null) return Errors.Users.NotFound;

            return result;
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
