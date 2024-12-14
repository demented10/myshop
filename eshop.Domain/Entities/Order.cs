namespace eshop.Domain.Entities
{
    public class Order
    {
        public required int Id { get; set; }
        public required DateTime OrderDate { get; set; }
        public required int UserId { get; set; }
        public virtual User? User { get; set; }
        public required int StatusId { get; set; }
        public virtual OrderStatus? Status { get; set; }
        public required decimal TotalAmount { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
