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
    public class UserRegistrationService
    {
        private readonly IUserRepository<User> _userRepository;
        public UserRegistrationService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserDto>> RegistrateUserAsync(UserDto userDto)
        {
            try
            {
                var user = new User
                {
                    Name = userDto.name,
                    Email = userDto.email

                };
                await _userRepository.CreateAsync(user);
                return Result.Ok(new UserDto(user.Id,user.Name, user.Email));

            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось создать пользователя").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
    }
}
