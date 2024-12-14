namespace eshop.Domain.Entities
{
    public class OrderItem
    {
        public required int Id { get; set; }
        public required int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}
