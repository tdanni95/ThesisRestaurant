﻿using ThesisRestaurant.Domain.CustomFoods;
using ThesisRestaurant.Domain.Orders.OrderCustomItems;
using ThesisRestaurant.Domain.Orders.OrderItems;

namespace ThesisRestaurant.Application.Cart.Common
{
    public record CartResult(List<OrderCustomItem> CustomFoods, List<OrderItem> Foods);
}
