using eshop.Application.eshop.Application.Products;
using eshop.Application.Users;
using eshop.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Common;
using NuGet.Protocol;
using System.Security.Claims;


namespace eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult Protected()
        {
            //
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return Json(new { userId, user});
        }
        
    }
}
