
using ErrorOr;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;

namespace ThesisRestaurant.Application.Common.Interfaces.Orders
{
    public interface IOrderCustomItemBuilder
    {
        public Task<ErrorOr<List<OrderCustomItem>>> BuildCustomItems(List<int> ids);
    }
}
