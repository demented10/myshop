﻿namespace eshop.Domain.Entities
{
    public class BasketItem
    {
        public required int Id { get; set; }
        public required int ShoppingCartId { get; set; }
        public virtual Basket? Basket { get; set; }
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public required int Quantity { get; set; }
    }
}
