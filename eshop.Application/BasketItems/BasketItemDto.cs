using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.BasketItems
{
    public record BasketItemDto(int BasketId, int ProductId, int Quantity);
}
