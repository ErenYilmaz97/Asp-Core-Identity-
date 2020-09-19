using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Şifre Zorunludur.")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password,ErrorMessage = "Şifre Uygun Değil.")]
        [MinLength(4,ErrorMessage = "Şifre En Az 4 Karakter Olmalıdır.")]

        public string NewPassword { get; set; }
    }
}
