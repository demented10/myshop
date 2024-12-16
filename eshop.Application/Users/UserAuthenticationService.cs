using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Users
{
    public class UserAuthenticationService
    {
        private readonly IUserRepository<User> _userRepository;
        public UserAuthenticationService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
