namespace eshop.Client.Models.Orders
{
    public record OrderItemResponse(int OrderId, int ProductId, int Quantity, decimal UnitPrice);
}
