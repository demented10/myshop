﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Users
{
   public record UserRegistrationDto(int id, string name, string email, string password);
}
