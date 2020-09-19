using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.SMTP
{
    public static class PasswordReset
    {
        public static void SendPasswordResetMail(string link,string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();

            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("erenbaba1212@gmail.com","03123897461eren");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
           


            mail.From = new MailAddress("erenbaba1212@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Asp Net Core Identity Şifre Sıfırlama";
            mail.Body = "<h2>Şifrenizi Sıfırlamak İçin Aşağıdaki Linke Tıklayınız</h2><hr/>";
            mail.Body += $"<a href ='{link}'> Şifre Değiştir</a>";
            mail.IsBodyHtml = true;

            
            client.Send(mail);
        }

        
    }
}
