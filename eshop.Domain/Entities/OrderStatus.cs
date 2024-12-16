namespace eshop.Domain.Entities
{
    public class OrderStatus
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
