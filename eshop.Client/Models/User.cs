using System.Security.Claims;

namespace eshop.Client.Models
{
    public class User
    {
        public string UserName { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public string Password { get; set; } = "";
        public List<string> Roles { get; set; } = new();
        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.Name, UserName),
            new (ClaimTypes.Email, UserEmail),
            new (ClaimTypes.Hash, Password)
        }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()), "jwt"));
        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            UserName = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            UserEmail = principal.FindFirst(ClaimTypes.Email)?.Value ?? "",
            Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
            Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        };
    }
}
