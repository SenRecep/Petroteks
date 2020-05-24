using Petroteks.Core.Entities;
using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class Website : EntityBase, IWebsite
    {
        public string BaseUrl { get; set; }
        public string Name { get; set; }
    }
}
