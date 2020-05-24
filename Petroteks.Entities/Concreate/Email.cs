using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class Email : WebsiteObject, IEmail
    {
        public string EmailAddress { get; set; }
        public string Category { get; set; }
    }
}
