namespace eshop.Domain.Entities
{
    public class Basket
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<BasketItem>? BasketItems { get; set; }
    }
}
