using System.Net;
using System.Net.Mail;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public static class Mail
    {
        public static void SendMail(string body)
        {
            MailAddress fromAddress = new MailAddress("petroteksmailsender@gmail.com", "PETROTEKS İLETİŞİM BİLDİRİMİ");
            MailAddress toAddress = new MailAddress("petroteksiletisim@gmail.com");
            const string subject = "PETROTEKS İletişim Bildirimi";
            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Password12*")
                //trololol kısmı e-posta adresinin şifresi
            };
            using SmtpClient smtp = smtpClient;
            using MailMessage message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body };
            smtp.Send(message);

        }
    }
}
