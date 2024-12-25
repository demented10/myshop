using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using Microsoft.Extensions.Configuration;


namespace eshop.Application.Users
{
    public class UserAuthenticationService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public UserAuthenticationService(IUserRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<Result<AuthResultDto>> AuthenticateUser(UserAuthenticationDto userDto) 
        {
            try
            {
                var user = await _userRepository.FindByEmailAsync(userDto.Email);
                if(user is null || !AuthenticationHelper.VerifyPassword(userDto.Password, user.PasswordHash))
                {
                    return Result.Fail("Пользователь не найден");
                }
                var token = AuthenticationHelper.GenerateJwtToken(user, _configuration["JWT:Secret"]);
                return Result.Ok(new AuthResultDto(true, token));
            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось авторизироваться").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
       

    }
}
