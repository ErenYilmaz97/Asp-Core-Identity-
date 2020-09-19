using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentity.CustomValidations
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "PasswordTooShort",
                Description = $"Şifre En Az {length} Karakter Olmalıdır."
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = "InvaliduserName",
                Description = "Kullanıcı Adı Geçersiz."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "DuplicateEmail",
                Description = "Mail Adresi Kullanılıyor."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "DuplicateUserName",
                Description = "Kullanıcı Adı Kullanılıyor."
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = "InvalidToken",
                Description = "Geçersiz Link."
            };
        }
    }
}
