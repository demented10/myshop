using eshop.Client.Models.Products;

namespace eshop.Client.Models.Baskets
{
    public class BasketItem
    {
        public Product product { get; set; } = new();
        public int Quantity { get; set; }
    }
}