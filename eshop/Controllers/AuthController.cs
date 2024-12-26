using eshop.Application.eshop.Application.Products;
using eshop.Application.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Common;


namespace eshop.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserAuthenticationService _userAuthenticationService;

        public AuthController(UserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }
        
        [HttpPost("auth")]
        public async Task<ActionResult<AuthResultDto>> Auth([FromBody]UserAuthenticationDto user)
        {
            var result = await _userAuthenticationService.AuthenticateUser(user);

            if (result.IsSuccess)
            {
                var token = result.Value.Token;
                return Json(new { Email = user.Email, Token = token });
            }

            return BadRequest(result.Errors);
        }
        [Authorize]
        [HttpGet("protected")]
        public async Task<ActionResult> Protected()
        {
            //
            return Ok(new { Message = "Protected resource accessed." });
        }
        [HttpPost("unauth")]
        public ActionResult UnAuth()
        {
                Response.Cookies.Delete(
                 "jwt"
                );
                return Ok();
        }
    }
}
