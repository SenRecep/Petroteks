using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Petroteks.Bll.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Petroteks.MvcUi.Models
{
    public class RouteTable : IRouteTable
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILanguageCookieService languageCookieService;
        private ICollection<RouteTableElement> RouteTableElements;
        private string tempFolder;
        private string websiteTempFile;

        private bool IsUnLoaded => (RouteTableElements == null || RouteTableElements.Count == 0);
        private bool folderPathsIsUnLoaded => (string.IsNullOrWhiteSpace(tempFolder) || string.IsNullOrWhiteSpace(websiteTempFile));

        private void folderPathLoad()
        {
            if (folderPathsIsUnLoaded)
            {
                tempFolder = Path.Combine(webHostEnvironment.WebRootPath, "Temp");
                websiteTempFile = Path.Combine(tempFolder, "RouteTable.json");
            }
        }


        public RouteTable(IServiceProvider serviceProvider)
        {
            webHostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();
            languageCookieService = serviceProvider.GetService<ILanguageCookieService>();
        }

        public void Load()
        {
            folderPathLoad();
            if (IsUnLoaded && !folderPathsIsUnLoaded)
            {
                if (File.Exists(websiteTempFile))
                {
                    RouteTableElements = JsonConvert.DeserializeObject<ICollection<RouteTableElement>>(File.ReadAllText(websiteTempFile));
                }
                else
                {
                    RouteTableElements = new List<RouteTableElement>() {
                        new RouteTableElement(EntityName.Product,"tr-TR",PageType.Detail,"Urun"),
                        new RouteTableElement(EntityName.Product,"en-US",PageType.Detail,"Product"),
                        new RouteTableElement(EntityName.Product,"en-GB",PageType.Detail,"Product"),
                        new RouteTableElement(EntityName.Product,"fr-FR",PageType.Detail,"Produit"),

                        new RouteTableElement(EntityName.Category,"tr-TR",PageType.Detail,"Kategori-Detay"),
                        new RouteTableElement(EntityName.Category,"en-US",PageType.Detail,"Category-Detail"),
                        new RouteTableElement(EntityName.Category,"en-GB",PageType.Detail,"Category-Detail"),
                        new RouteTableElement(EntityName.Category,"fr-FR",PageType.Detail,"Categorie-Detai"),


                        new RouteTableElement(EntityName.Category,"tr-TR",PageType.Normal,"Kategori"),
                        new RouteTableElement(EntityName.Category,"en-US",PageType.Normal,"Category"),
                        new RouteTableElement(EntityName.Category,"en-GB",PageType.Normal,"Category"),
                        new RouteTableElement(EntityName.Category,"fr-FR",PageType.Normal,"Categorie"),

                        new RouteTableElement(EntityName.Blog,"tr-TR",PageType.List,"Bloglar"),
                        new RouteTableElement(EntityName.Blog,"en-US",PageType.List,"Blogs"),
                        new RouteTableElement(EntityName.Blog,"en-GB",PageType.List,"Blogs"),
                        new RouteTableElement(EntityName.Blog,"fr-FR",PageType.List,"Blogs"),

                        new RouteTableElement(EntityName.Blog,"tr-TR",PageType.Detail,"Blog-Detay"),
                        new RouteTableElement(EntityName.Blog,"en-US",PageType.Detail,"Blog-Detail"),
                        new RouteTableElement(EntityName.Blog,"en-GB",PageType.Detail,"Blog-Detail"),
                        new RouteTableElement(EntityName.Blog,"fr-FR",PageType.Detail,"Blog-Detail"),
                    };
                    string json = JsonConvert.SerializeObject(RouteTableElements, Formatting.Indented);
                    File.WriteAllText(websiteTempFile, json);
                }
            }
        }
        public string Get(EntityName entityName, PageType pageType)
        {
            if (IsUnLoaded)
            {
                Load();
                return Get(entityName, pageType);
            }
            else
            {
                return RouteTableElements?.FirstOrDefault(x =>
                   x.EntityName == entityName &&
                   x.LanguageKeyCode == languageCookieService.Get("CurrentLanguage").KeyCode &&
                   x.PageType == pageType)?.Content;
            }
        }

        public string Get(EntityName entityName, PageType pageType, string langCode)
        {
            if (IsUnLoaded)
                Load();

            return RouteTableElements?.FirstOrDefault(x =>
               x.EntityName == entityName &&
               x.LanguageKeyCode == langCode &&
               x.PageType == pageType)?.Content;
        }

        public bool Exists(string content, EntityName entityName, PageType pageType)
        {
            if (IsUnLoaded)
                Load();

            return RouteTableElements?.FirstOrDefault(x =>
                    x.Content.Equals(content, StringComparison.CurrentCultureIgnoreCase) &&
                    x.EntityName.Equals(entityName) &&
                    x.PageType.Equals(pageType)) != null;

        }
    }
    public interface IRouteTable
    {
        void Load();
        string Get(EntityName entityName, PageType pageType);
        string Get(EntityName entityName, PageType pageType, string langCode);
        bool Exists(string content, EntityName entityName, PageType pageType);
    }
}
