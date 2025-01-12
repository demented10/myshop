using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Users
{
   public record UserRegistrationDto(int Id, string Name, string Email, string Password);
}
