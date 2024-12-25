using eshop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public Task<string> GenerateJwtToken(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
