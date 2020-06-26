using System;
using System.Collections.Generic;
using System.Text;

namespace Petroteks.Bll.Concreate
{
    public class RouteTableElement
    {
        public RouteTableElement(EntityName entityName, string languageKeyCode, PageType pageType, string content)
        {
            this.Content = content;
            this.LanguageKeyCode = languageKeyCode;
            this.PageType = pageType;
            this.EntityName = entityName;
        }
        public EntityName EntityName { get; set; }
        public string LanguageKeyCode { get; set; }
        public PageType PageType { get; set; }
        public string Content { get; set; }
    }
    public enum PageType
    {
        List,
        Detail,
        Normal
    }
    public enum EntityName
    {
        AboutUsObject,
        PrivacyPolicyObject,
        MainPage,
        Category,
        Product,
        Blog,
        DynamicPage,
        UI_Contact,
    }
}
