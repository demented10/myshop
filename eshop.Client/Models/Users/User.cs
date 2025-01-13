using System.Security.Claims;

namespace eshop.Client.Models.Users
{
    public class User
    {
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public List<string> Roles { get; set; } = new();
        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.NameIdentifier, UserId.ToString()),
            new (ClaimTypes.Name, UserName),
            new (ClaimTypes.Email, UserEmail),
        }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()), "jwt"));
        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            UserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "",
            UserName = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            UserEmail = principal.FindFirst(ClaimTypes.Email)?.Value ?? "",
            Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        };
    }
}
