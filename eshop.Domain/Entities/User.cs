﻿namespace eshop.Domain.Entities
{
    public class User
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
