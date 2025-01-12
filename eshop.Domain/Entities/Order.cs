namespace eshop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public required DateTime OrderDate { get; set; }
        public required int UserId { get; set; }
        public virtual User? User { get; set; }
        public required OrderStatus Status { get; set; }
        public required decimal TotalAmount { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
