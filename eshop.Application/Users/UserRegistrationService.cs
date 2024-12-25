using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using BCrypt.Net;

namespace eshop.Application.Users
{
    public partial class UserRegistrationService
    {
        private readonly IUserRepository<User> _userRepository;
        public UserRegistrationService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserDto>> RegistrateUserAsync(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var existingUser = await _userRepository.FindByEmailAsync(userRegistrationDto.Email);
                if (existingUser is not null)
                {
                    //return RegistrationResult.Failure("IsExists", 409);
                    return Result.Fail("Пользователь уже существует");
                }
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.Password);

                var user = new User
                {
                    Name = userRegistrationDto.Name,
                    Email = userRegistrationDto.Email,
                    PasswordHash = hashedPassword,
                    IsVerified = false
                };
                await _userRepository.CreateAsync(user);
                return Result.Ok(new UserDto(userRegistrationDto.Id,userRegistrationDto.Name, userRegistrationDto.Email));

            }
            catch(Exception ex)
            {
                //return RegistrationResult.Failure($"{ex.Message}\n{ex.StackTrace}", 500);
                return Result.Fail("Не удалось зарегистрировать пользователя").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
    }
}
