using Microsoft.AspNetCore.Http;
using Petroteks.Entities.Concreate;
using System.Collections.Generic;

namespace Petroteks.MvcUi.Areas.Admin.Models
{
    public class MailViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile File { get; set; }
        public ICollection<Email> Emails { get; set; }
    }
}
