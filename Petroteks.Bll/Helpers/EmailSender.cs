using Petroteks.Entities.Concreate;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System;
using Petroteks.Bll.Abstract;
using System.Net;

namespace Petroteks.Bll.Helpers
{
    public class EmailSender
    {
        public string Host { get; set; } = "smtp-mail.outlook.com";
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string CredentialEmailAdress { get; set; } = "isim@outlook.com";
        public string CredentialEmailAdressPassword { get; set; } = "parola";
        public string CredentialDisplay { get; set; } = "Petroteks.com";
        public string Subject { get; set; }
        public string Body { get; set; }


        private readonly IEmailService emailService;

        public EmailSender(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public string EmailControl(params string[] emails)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string email in emails)
            {
                bool Status = RegexUtilities.IsValidEmail(email);
                if (Status == false)
                    stringBuilder.Append($"{email},");
            }
            if (stringBuilder.ToString().EndsWith(","))
                stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
            if (!string.IsNullOrWhiteSpace(stringBuilder.ToString()))
                stringBuilder.AppendLine("Email adresleri hatalı.");
            return stringBuilder.ToString();
        }

        public void EmailAdd(Website Website, params string[] emails)
        {
            if (emails.Length > 0)
            {
                foreach (string email in emails)
                    emailService.Add(new Email() { EmailAddress = email, WebSite = Website });
                emailService.Save();
            }
        }

        public ICollection<Email> LoadWebsiteEmails(int WebsiteId) => emailService.GetMany(x => x.WebSiteid == WebsiteId);

        private string EmailsToString(ICollection<Email> emails)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Email email in emails)
                stringBuilder.Append($"{email}, ");
            if (stringBuilder.ToString().EndsWith(", "))
                stringBuilder = stringBuilder.Remove(stringBuilder.Length - 2, 2);
            return stringBuilder.ToString();
        }

        public bool Send(ICollection<Email> emails, params string[] files)
        {
            if (emails.Count == 0)
                return false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Port;
            smtp.EnableSsl = EnableSsl;
            smtp.Credentials = new NetworkCredential(CredentialEmailAdress, CredentialEmailAdressPassword);
            MailMessage email = new MailMessage();
            email.From = new MailAddress(CredentialEmailAdress, CredentialDisplay);
            email.To.Add(EmailsToString(emails));
            email.Subject = Subject;
            email.Body = Body;
            foreach (string file in files)
                email.Attachments.Add(new Attachment(file));
            try
            {
                smtp.Send(email);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
