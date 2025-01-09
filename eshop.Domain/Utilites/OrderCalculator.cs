using eshop.Domain.Entities;

namespace eshop.Domain.Utilites
{
    public static class OrderCalculator
    {
        public static decimal CalculateTotalCost(ICollection<BasketItem> basketItems)
        {
            return basketItems.Sum(bi => bi.Quantity * bi.Product.Price);
        }
    }
}
