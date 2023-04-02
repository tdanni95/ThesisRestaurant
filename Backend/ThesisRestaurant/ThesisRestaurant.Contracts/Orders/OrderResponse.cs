namespace ThesisRestaurant.Contracts.Orders
{
    public record OrderResponse(string User, List<ActualOrderResponse> Items);

    public record ActualOrderResponse(int Id, DateTime Created, string Address, CartResponse Items);
}
