using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Eski Şifrenizi Giriniz.")]
        [DisplayName("Eski Şifre")]
        [DataType(DataType.Password,ErrorMessage = "Şifre Uygun Değil.")]
        [MinLength(4,ErrorMessage = "Eski Şifre En Az 4 Karakter Olmalıdır.")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Yeni Şifrenizi Giriniz.")]
        [DisplayName("Yeni Şifre")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Yeni Şifre En Az 4 Karakter Olmalıdır.")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Yeni Şifrenizi Tekrar Giriniz.")]
        [DisplayName("Eski Şifre (Tekrar)")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Şifreler Uyuşmuyor.")]
        public string PasswordConfirm { get; set; }
    }
}
