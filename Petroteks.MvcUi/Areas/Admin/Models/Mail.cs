using System.Net;
using System.Net.Mail;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public static class Mail
    {
        public static void SendMail(string body)
        {
            MailAddress fromAddress = new MailAddress("petroteksiletisim@gmail.com", "PETROTEKS İLETİŞİM BİLDİRİMİ");
            MailAddress toAddress = new MailAddress("petroteksiletisim@gmail.com");
            const string subject = "PETROTEKS İletişim Bildirimi";
            using (SmtpClient smtp = new SmtpClient
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
                using (MailMessage message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }

        }
    }
}
