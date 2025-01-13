namespace eshop.Client.Models.Orders
{
    public record OrderResponse(int Id, int UserId, DateTime DateTime, string OrderStatus, decimal TotalPrice);
}
