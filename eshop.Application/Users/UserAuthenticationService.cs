using eshop.Domain.Entities;
using eshop.Domain.Repositories;
using FluentResults;
using JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


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

        public async Task<Result<TokenDto>> AuthenticateUser(UserAuthenticationDto userDto) 
        {
            try
            {
                var user = await _userRepository.FindByEmailAsync(userDto.Email);
                if(user is null || !AuthenticationHelper.VerifyPassword(userDto.Password, user.PasswordHash))
                {
                    return Result.Fail("Пользователь не найден");
                }
                var token = AuthenticationHelper.GenerateJwtToken(user, _configuration["JWT:Secret"]);
                return Result.Ok(new TokenDto(token));
            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось авторизироваться").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
        public async Task<Result> ValidateJwtTokenAsync(TokenDto tokenDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            };
            try
            {
                var principal = await tokenHandler.ValidateTokenAsync(tokenDto.Token, validationParameters);
                if (principal.IsValid)
                    return Result.Ok();
                else
                    throw principal.Exception;
            }
            catch (SecurityTokenExpiredException)
            {
                return Result.Fail("Token expired.");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return Result.Fail("Invalid signature.");
            }
            catch (Exception ex)
            {
                return Result.Fail($"Unexpected error: {ex.Message}");
            }
        }
    }
}
