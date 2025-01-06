using eshop.Client.Models.Basket;

internal static class BasketServiceHelpers
{

    public static Basket MergeBaskets(Basket serverBasket, Basket clientBasket)
    {

        foreach (var item in serverBasket.BasketItems)
        {
            if (!clientBasket.BasketItems.Any(i => i.product.Id == item.product.Id))
            {
                clientBasket.BasketItems.Add(item);
            }
        }
        return clientBasket;
    }
}