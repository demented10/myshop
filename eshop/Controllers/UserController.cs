using eshop.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace eshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserRegistrationService _userRegistrationService;

        public UserController(UserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserDto userDto)
        {
            var result = await _userRegistrationService.RegistrateUserAsync(userDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(Create), new { result.Value.id}, result.Value);
            }
            return BadRequest(result.Errors);
        }

    }
}
