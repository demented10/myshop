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
        public async Task<RegistrationResult> RegistrateUserAsync(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var existingUser = await _userRepository.FindByEmailAsync(userRegistrationDto.email);
                if(existingUser is not null)
                {
                    return RegistrationResult.Failure("Данный Email уже занят.", 409);
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.password);

                var user = new User
                {
                    Name = userRegistrationDto.name,
                    Email = userRegistrationDto.email,
                    PasswordHash = hashedPassword,
                    isVerified = false
                };
                await _userRepository.CreateAsync(user);
                return RegistrationResult.Success();

            }
            catch(Exception ex)
            {
                return RegistrationResult.Failure($"{ex.Message}\n{ex.StackTrace}", 500);
            }
        }
    }
}
