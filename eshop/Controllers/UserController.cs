using eshop.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserRegistrationService _userRegistrationService;
        private readonly GetUsersService _getUsersService;

        public UserController(UserRegistrationService userRegistrationService, GetUsersService getUsersService)
        {
            _userRegistrationService = userRegistrationService;
            _getUsersService = getUsersService;
        }


        [HttpPost("registration")]
        public async Task<IActionResult> Create([FromBody] UserRegistrationDto registrationUserDto)
        {
            var result = await _userRegistrationService.RegistrateUserAsync(registrationUserDto);

            if (result.IsSuccess)
            {
                return Ok("Регистрация прошла успешно");
            }
            return StatusCode((int)result.ErrorCode, result.ErrorMessage);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var result = await _getUsersService.GetAllUsersAsync(CancellationToken.None);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

    }
}
