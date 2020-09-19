using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Entity.DbContext;
using AspNetCoreIdentity.Entity.Entities;
using AspNetCoreIdentity.Enums;

namespace AspNetCoreIdentity.Models
{
    public class GetUsersModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }
    }
}
