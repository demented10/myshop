using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Domain
{
    public interface IJwtTokenGenerator
    {
        public Task<string> GenerateJwtToken(int userId);
    }
}
