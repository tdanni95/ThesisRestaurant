using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasMany(oi => oi.AdditionalIngredients);
            builder.HasOne(oi => oi.FoodSize).WithMany();
        }
    }
}
