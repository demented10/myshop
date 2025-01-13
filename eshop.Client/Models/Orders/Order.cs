namespace eshop.Client.Models.Orders
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();

    }
}
