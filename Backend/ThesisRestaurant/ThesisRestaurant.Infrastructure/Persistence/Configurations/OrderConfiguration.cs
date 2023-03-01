using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThesisRestaurant.Domain.Orders;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.Foods).WithMany(f => f.Orders).UsingEntity<OrderItem>();

            builder.HasMany(o => o.CustomFoods).WithMany(cf => cf.Orders).UsingEntity<OrderCustomItem>(); 
        }
    }
}
