using System.ComponentModel.DataAnnotations.Schema;

namespace eshop.Domain.Entities
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual IEnumerable<Product>? Products { get; set; }
    }
}

