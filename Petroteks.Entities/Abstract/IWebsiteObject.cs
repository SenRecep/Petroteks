namespace Petroteks.Entities.Abstract
{
    public interface IWebsiteObject
    {
        int WebSiteid { get; set; }
        IWebsite WebSite { get; set; }
    }
}
