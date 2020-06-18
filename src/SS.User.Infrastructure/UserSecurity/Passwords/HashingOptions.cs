using System;
using System.Collections.Generic;
using System.Text;

namespace SS.Users.Infrastructure.UserSecurity.Passwords
{
     public sealed class HashingOptions
     {
        public int Iterations { get; set; } = 10000;
     }
}
