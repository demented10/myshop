using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Domain.Repositories
{
   public interface IBasketItemRepository<BasketItem> : IRepository<BasketItem>
    {
        Task<BasketItem> ChangeQuantityAsync(int basketId, int productId, int quantity);
        Task<BasketItem> GetByIdAsync(int basketId, int productId, CancellationToken cancellationToken = default);
        Task DeleteAsync(int basketId, int productId); 
    }
}
