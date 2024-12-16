using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Users
{
    public class GetUsersService
    {
        private readonly IUserRepository<User> _userService;
        public GetUsersService(IUserRepository<User> userService)
        {
            _userService = userService;
        }

    }
}
