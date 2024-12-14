namespace eshop.Domain.Entities
{
    public class OrderStatus
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
