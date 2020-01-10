using Petroteks.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Entities.Concreate
{
    public class Email : WebsiteObject, IEmail
    {
        public string EmailAddress { get; set; }
        public string Category { get; set; }
    }
}
