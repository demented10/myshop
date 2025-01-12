using System.Net.Http.Headers;

namespace eshop.Client.Models.Basket
{
    public class BasketItem
    {
        public Product product { get; set; } = new();
        public int Quantity { get; set; }
    }
}