namespace ThesisRestaurant.Contracts.Orders
{
    public record OrderResponse(int Id, string Address, DateTime Created, CartResponse Items);
}
