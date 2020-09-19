using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur.")]
        [MaxLength(20,ErrorMessage = "Kullanıcı Adı 20 Karakteri Geçemez.")]
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [DisplayName("Telefon")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "Doğru Telefon Numarası Giriniz.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "E-Mail Zorunludur.")]
       [EmailAddress(ErrorMessage = "Email Doğru Formatta Değil")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Zorunludur.")]
        [DataType(DataType.Password,ErrorMessage = "Doğru Şifre Giriniz.")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

    }
}
