using Petroteks.Bll.Abstract;
using Petroteks.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Petroteks.Bll.Helpers
{
    public class EmailSender
    {
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string CredentialEmailAdress { get; set; } = "incecamberat@gmail.com";
        public string CredentialEmailAdressPassword { get; set; } = "12ve23kez";
        public string CredentialDisplay { get; set; } = "Petroteks.com";
        public string Subject { get; set; }
        public string Body { get; set; } = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" align=\"center\" bgcolor=\"#ebebeb\"> <tbody><tr> <td> <table width=\"900\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\"> <tbody><tr> <td colspan=\"3\"><img style=\"display:block\" border=\"0\" src=\"/Admin/assets/images/mailpage.png\" width=\"900\" height=\"400\" alt=\"\"  tabindex=\"0\"></td> </tr> <tr> <td><img style=\"display:block\" border=\"0\" src=\"/Admin/assets/images/solcizg.png\" width=\"125\" height=\"400\" alt=\"\" class=\"CToWUd\"></td> <td width=\"650\" height=\"400\" bgcolor=\"#FFFFFF\" align=\"center\"><font face=\"Helvetica Neue, Helvetica, Arial\" color=\"#1b4772\" size=\"5\"><strong><span class=\"il\">Petroteks</span> Bilgilendirme</strong></font> <br> <br> <font face=\"Helvetica Neue, Helvetica, Arial\" color=\"#333333\"></font> <p style=\"line-height:23px\"><font face=\"Helvetica Neue, Helvetica, Arial\" color=\"#333333\"><span class=\"il\">Bilgilendirme yazısı Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</font></p> <br> <br> <br> <a href=\"#m_-5161798783556842402_\" style=\"text-decoration:none\"><img style=\"display:block\" border=\"0\" src=\"/Admin/assets/images/btn.png\" width=\"250\" height=\"75\" class=\"CToWUd\"></a> </td> <td><img style=\"display:block\" border=\"0\" src=\"/Admin/assets/images/sagcizg.png\" width=\"125\" height=\"400\" alt=\"\" class=\"CToWUd\"></td> </tr> <tr> <td colspan=\"3\"><img style=\"display:block\" border=\"0\" src=\"/Admin/assets/images/altcizg.png\" width=\"900\" height=\"175\" alt=\"\" class=\"CToWUd a6T\" tabindex=\"0\"> </td> </tr> </tbody></table> </td> </tr> </tbody></table>";


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
                {
                    stringBuilder.Append($"{email},");
                }
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }

            if (!string.IsNullOrWhiteSpace(stringBuilder.ToString()))
            {
                stringBuilder.AppendLine("Email adresleri hatalı.");
            }

            return stringBuilder.ToString();
        }

        public bool EmailAdd(Website Website, string email, string category)
        {
            bool status = false;
            if (emailService.Get(x => x.EmailAddress == email && x.WebSiteid == Website.id) == null)
            {
                status = true;
                emailService.Add(new Email() { EmailAddress = email, Category = category, WebSite = Website });
                try
                {
                    status = true;
                    emailService.Save();
                }
                catch (Exception)
                {
                    status = false;
                    throw;
                }

            }
            return status;
        }

        public ICollection<Email> LoadWebsiteEmails(int WebsiteId)
        {
            return emailService.GetMany(x => x.WebSiteid == WebsiteId && x.IsActive == true);
        }

        private string EmailsToString(ICollection<Email> emails)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Email email in emails)
            {
                stringBuilder.Append($"{email.EmailAddress}, ");
            }

            if (stringBuilder.ToString().EndsWith(", "))
            {
                stringBuilder = stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            return stringBuilder.ToString();
        }

        public bool Send(ICollection<Email> emails, params Attachment[] files)
        {
            if (emails.Count == 0)
            {
                return false;
            }

            SmtpClient smtp = new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = EnableSsl,
                Credentials = new NetworkCredential(CredentialEmailAdress, CredentialEmailAdressPassword)
            };
            MailMessage email = new MailMessage
            {
                From = new MailAddress(CredentialEmailAdress, CredentialDisplay)
            };
            string EmailString = EmailsToString(emails);
            email.To.Add(EmailString);
            email.Subject = Subject;
            email.Body = Body;
            email.IsBodyHtml = true;
            foreach (Attachment file in files)
            {
                if (file != null)
                {
                    email.Attachments.Add(file);
                }
            }

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
