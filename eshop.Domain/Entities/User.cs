using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace eshop.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public required string Email { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
