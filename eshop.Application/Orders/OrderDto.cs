using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Orders
{
    public record OrderDto(int Id, int UserId, DateTime DateTime, string OrderStatus, decimal TotalPrice);
}
