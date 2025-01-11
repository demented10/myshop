using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.OrderItems
{
    public record OrderItemDto(int OrderId, int ProductId, int Quantity, decimal UnitPrice);
}
