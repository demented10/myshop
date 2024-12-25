using eshop.Domain.Entities;
using eshop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Domain.Repositories
{
    public interface IUserRepository<User> : IRepository<User>
    {
        public Task<User> FindByEmailAsync(string email);
    }
}
