using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Enums;
using Microsoft.VisualBasic.CompilerServices;

namespace AspNetCoreIdentity.Models
{
    public class UserEditModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur.")]
        [DisplayName("Kullanıcı Adı")]
        [MaxLength(20)]
        public string UserName { get; set; }



        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }

        
        [DisplayName("Şehir")]
        [MaxLength(50)]
        public string City { get; set; }
        
        [DisplayName("Resim")]
        public string Picture { get; set; }

        [DisplayName("Doğum Günü")]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }


        [DisplayName("Cinsiyet")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }


        
        

    }
}
