using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Enums;

namespace AspNetCoreIdentity.Models
{
    public class UserInfoModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur.")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        
        [DisplayName("Email Adresi")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Picture { get; set; }

        
        public DateTime? BirthDay { get; set; }

        public Gender Gender { get; set; }
    }
}
