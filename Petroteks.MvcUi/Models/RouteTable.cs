using Microsoft.AspNetCore.Hosting;
using Petroteks.Bll.Concreate;
using System;
using System.Collections.Generic;
using System.IO;
using Petroteks.MvcUi.ExtensionMethods;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Petroteks.MvcUi.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
                    RouteTableElements = JsonConvert.DeserializeObject<ICollection<RouteTableElement>>(File.ReadAllText(websiteTempFile));
                else
                {
                    RouteTableElements = new List<RouteTableElement>() {
                        new RouteTableElement(EntityName.Product,"tr-TR",PageType.Detail,"Urun-Detay"),
                        new RouteTableElement(EntityName.Product,"en-US",PageType.Detail,"Product-Detail"),
                        new RouteTableElement(EntityName.Category,"tr-TR",PageType.Detail,"Kategori-Detay"),
                        new RouteTableElement(EntityName.Category,"en-US",PageType.Detail,"Category-Detail"),
                        new RouteTableElement(EntityName.Blog,"tr-TR",PageType.List,"Bloglar"),
                        new RouteTableElement(EntityName.Blog,"en-US",PageType.List,"Blogs"),
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
                return RouteTableElements?.FirstOrDefault(x =>
                   x.EntityName == entityName &&
                   x.LanguageKeyCode == languageCookieService.Get("CurrentLanguage").KeyCode &&
                   x.PageType == pageType)?.Content;

        }

        public bool Exists(string content, EntityName entityName, PageType pageType) =>
            RouteTableElements.FirstOrDefault(x =>
            x.Content.Equals(content) &&
            x.EntityName.Equals(entityName)  &&
            x.PageType.Equals(pageType)) != null;
    }
    public interface IRouteTable
    {
        void Load();
        string Get(EntityName entityName, PageType pageType);
        bool Exists(string content, EntityName entityName, PageType pageType);
    }
}
