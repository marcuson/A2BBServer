using A2BBCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBIdentityServer.Models
{
    public class NewUserRequestDTO
    {
        public User User { get; set; }
        public String Password { get; set; }
    }
}
