using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using GCSAPI;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;

namespace Petroteks.MvcUi.Controllers
{
    [EnableCors("MyPolicy")]
    public class SearchController : GlobalController
    {
        private readonly SearchManager searchManager;
        private readonly CustomSearchApi gcsapi;

        public SearchController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            gcsapi = serviceProvider.GetService<CustomSearchApi>();
            searchManager = serviceProvider.GetService<SearchManager>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        private SearchViewModel classicSearch(string s)
        {
            searchManager.Build(base.CurrentWebsiteId, base.CurrentLanguageId);
            var result = searchManager.Search(s);
            return new SearchViewModel()
            {
                SearchKey = s,
                Result = result
            };
        }
        private async Task<List<GcsEntity>> googleSearch(string s)
        {
            var entities = await gcsapi.Search(s);
            ViewBag.SearchKey = s;
            ViewBag.Data = entities?.Count;
            return entities;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string s, string searchType)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                switch (searchType)
                {
                    case SearchTypeInfo.Classic:
                    default:
                        return View("ClassicSearchResultPage", classicSearch(s));
                    case SearchTypeInfo.Google:
                       var googleResponse= await googleSearch(s);
                        if (googleResponse!=null)
                            return View("GoogleSearchResult", googleResponse);
                        else
                        return View("ClassicSearchResultPage", classicSearch(s));
                }
            }
            else{
                return View();
            }

        }
    }
}
