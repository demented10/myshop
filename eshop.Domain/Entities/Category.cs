using System.ComponentModel.DataAnnotations.Schema;

namespace eshop.Domain.Entities
{
    [Table("Categories")]
    public class Category
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
