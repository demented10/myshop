using eshop.Client.Models.Products;

namespace eshop.Client.Models.Orders
{
    public class OrderItem
    {
        public Product product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
