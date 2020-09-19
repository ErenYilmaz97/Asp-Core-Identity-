using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email Giriniz.")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Email Adresini Doğru Formatta Değil.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Giriniz.")]
        [DataType(DataType.Password)]
        [DisplayName("Şifre")]
        [MinLength(4,ErrorMessage = "Şifre En Az 4 Karakter Uzunluğunda Olmalıdır.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
