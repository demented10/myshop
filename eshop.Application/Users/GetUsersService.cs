using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
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

        public async Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userService.GetAllAsync(cancellationToken);

                return Result.Ok(users.Select(u => new UserDto(u.Id,u.Name,u.Email)));
            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось получить список пользователей").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }

    }
}
