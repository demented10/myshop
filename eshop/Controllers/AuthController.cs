using eshop.Application.eshop.Application.Products;
using eshop.Application.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;


namespace eshop.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly UserAuthenticationService _userAuthenticationService;

        public AuthController(UserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDto>> Login([FromBody] UserAuthenticationDto user)
        {
            var result = await _userAuthenticationService.AuthenticateUser(user);

            if (result.IsSuccess)
            {
                Response.Cookies.Append(
                    "jwt",
                    result.Value.Token,
                    new CookieOptions { HttpOnly = true, Secure = true,  }
                    );
                return Ok();
            }

            return BadRequest(result.Errors);
        }
        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok(new { Message = "Protected resource accessed." });
        }
    }
}
