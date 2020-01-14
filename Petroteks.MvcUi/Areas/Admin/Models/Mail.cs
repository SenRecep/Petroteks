using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public static class Mail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("petroteksiletisim@gmail.com", "PETROTEKS İLETİŞİM BİLDİRİMİ");
            var toAddress = new MailAddress("petroteksiletisim@gmail.com");
            const string subject = "PETROTEKS İletişim Bildirimi";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "1parola1")
                //trololol kısmı e-posta adresinin şifresi
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }

        }
    }
}
