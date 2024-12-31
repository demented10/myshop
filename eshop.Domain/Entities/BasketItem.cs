namespace eshop.Domain.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public required int BasketId { get; set; }
        public virtual Basket? Basket { get; set; }
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public required int Quantity { get; set; }
    }
}
