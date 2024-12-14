using System.ComponentModel.DataAnnotations.Schema;

namespace eshop.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required virtual Category Category { get; set; }
    }
}
