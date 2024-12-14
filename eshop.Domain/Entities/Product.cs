using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eshop.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public required string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
