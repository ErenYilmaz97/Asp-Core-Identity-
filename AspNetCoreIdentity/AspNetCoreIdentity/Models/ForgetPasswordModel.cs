using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage ="Lütfen E-Postanızı Giriniz.")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "E-Posta Doğru Formatta Değil.")]
        public string Email { get; set; }
    }
}
