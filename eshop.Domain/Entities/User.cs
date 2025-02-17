﻿using Microsoft.EntityFrameworkCore;
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
        [EmailAddress]
        [MaxLength(200)]
        public required string Email { get; set; }
        [Required]
        [MinLength(8)]
        public required string PasswordHash { get; set; }
        public bool IsVerified { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
