using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using eshop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace eshop.Application.Users
{
    public class UserAuthenticationService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserAuthenticationService(IUserRepository<User> userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result> AuthenticateUser(UserAuthenticationDto userDto) 
        {
            try
            {
                var user = _userRepository.FindByEmailAsync(userDto.Email);
                if(user is null)
                {
                    return Result.Fail("Пользователь не найден");
                }

                var token = _jwtTokenGenerator.GenerateJwtToken(user.Id);


                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось авторизироваться");
            }
        }
    }
}
