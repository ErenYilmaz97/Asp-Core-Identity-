using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class RoleAssignModel
    {
        public RoleAssignModel()
        {
            Roles = new List<Role>();
        }

        public string UserID { get; set; }
        public string Username { get; set; }
        public List<Role> Roles { get; set; }
    }
}
