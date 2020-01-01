using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class SliderObject : WebsiteObject, IHtmlObject
    {
        public string Content { get; set; }
    }





    public class AboutUsObject : BasePage, IHtmlObject
    {
        public string Content { get; set; }
    }






    public class PrivacyPolicyObject : WebsiteObject, IHtmlObject
    {
        public string Content { get; set; }
    }
}
