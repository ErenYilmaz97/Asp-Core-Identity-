using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Exist { get; set; }
    }
}
